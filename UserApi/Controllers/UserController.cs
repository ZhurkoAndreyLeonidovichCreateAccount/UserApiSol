using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Entities.RequestFeatures;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace UserApi.Controllers

//Все exception проверяются Middleware в папке Extensions
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public UserController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <param name="userParameters"></param>
        /// <returns>Gets the list of All User</returns>
        /// <remarks>
        ///localhost/api/users?minAge=30 &amp; maxAge=45 &amp; Name=am &amp;orderBy=name,age desc &amp; pageNumber=1 &amp; pageSize=5 - Full request
        ///minAge=30 &amp; maxAge=45 - filter only age. You can only set the minimum age or maximum age
        ///Name=am -  Finds all names that contain "am". You can apply it for Name, Email, Role properties
        ///orderBy=name,age desc - Sorts the name in ascending order, age in descending order 
        ///pageNumber=1 &amp; pageSize=5 - Pagination
        /// </remarks>
        /// <response code="200">Returns all users</response>
        /// <response code="400">If max age less than min age in filtration</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
        {
            if (!userParameters.ValidAgeRange)
            {
                return BadRequest("Max age can't be less than min age.");
            }
                
            var users = await _repository.User.GetAllUsersAsync(userParameters,trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersDto);
        }
        /// <summary>
        /// Get a User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets the user with the given ID </returns>
        /// <response code="200">Returns one user</response>
        /// <response code="404">If user not found in database</response>

        [HttpGet("{id}", Name = "UserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.User.GetUserAsync(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");

                return NotFound();
            }
            else
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
        }


        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A newly created User</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "name": "Olga",
        ///        "age": 33,
        ///        "email": "user@example.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the user is null</response>
        /// <response code="422">If the user is not validated</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto user)
        {
            if (user == null)
            {
                _logger.LogError(" User object sent from client is null.");
                return BadRequest("User object is null");

            }
            

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the User object");
                return UnprocessableEntity(ModelState);
            }

            if (await _repository.User.GetUserByEmailAsync(user.Email , false) != null)
            {
                _logger.LogError("Invalid model state for the User object");
                ModelState.AddModelError("Email", "This email already exist");
                return UnprocessableEntity(ModelState);
               
            }
            
            var userEntity = _mapper.Map<User>(user);
          
            _repository.User.CreateUser(userEntity);
            await _repository.SaveAsync();

            var userToReturn = _mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("UserById", new { id = userToReturn.Id }, userToReturn);

        }

        /// <summary>
        /// Deletes a User with specific Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">if user is deleted succsesfully</response>
        /// <response code="404">If user not found in database</response>
        [Authorize(Roles = "Admin" )]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repository.User.GetUserAsync(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.User.DeleteUser(user);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a User with specific Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="204">if user is updated succsesfully</response>
        /// <response code="404">If user not found in database</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto user)
        {
            if (user == null)
            {
                _logger.LogError(" User object sent from client is null.");
                return BadRequest("User object is null");
            }

            var userEntity = await _repository.User.GetUserAsync(id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the User object");
                return UnprocessableEntity(ModelState);
            }

            if (await _repository.User.GetUserByEmailAsync(user.Email, false) != null)
            {
                _logger.LogError("Invalid model state for the User object");
                ModelState.AddModelError("Email", "This email already exist");
                return UnprocessableEntity(ModelState);

            }

            _mapper.Map(user, userEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Adds a role to user object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <response code="204">If the user role is successfully added</response>
        /// <response code="400">If roleName is empty or user has a role with this name already</response>
        /// <response code="404">If user or role not found in database</response>
        [HttpPut("{id}/{roleName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddRoleToUser(int id, string roleName)
       {
            if (string.IsNullOrEmpty(roleName))
            {
                _logger.LogError(" Role name sent from client is null.");
                return BadRequest("Role name is null");
            }

            var roles = _repository.Role.GetRoleByName(roleName, trackChanges:false);
            if (roles == null)
            {
                _logger.LogInfo($"Role with name: {roleName} doesn't exist in the database.");
                return NotFound("Role with this name doesn't exist");
            }

            var user = await _repository.User.GetUserAsync(id, trackChanges: true);
            if( user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            if (user!.Roles!.Any(r => r.RoleName.Equals(roleName)))
            {
                _logger.LogInfo($"User with id:{id} has a role with this name: {roleName}");
                return BadRequest("User has a role with this name already");
            }

            user!.Roles!.Add(roles);
            await _repository.SaveAsync();
            return NoContent();
        }

        

    }
}
