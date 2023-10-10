using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects.Auth;
using AutoMapper;
using Contracts;
using UserApi.Helpers;

namespace UserApi.Controllers
{
    //Добавил простую регистрацию с двумя ролями user и admin.
    //Админа зарегал сразу остальные пользователи добавляются c ролью User.
    //Не стал использовать Identity таблицы потому что их названия пересекаются с названиями которые в задании
    
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public AuthController(IConfiguration config, IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _config = config;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegistrationAsync([FromBody] UserReg user)
        {
            if(user == null)
            {
                _logger.LogError(" Registration object sent from client is null.");
                return BadRequest("Registration object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Registration object");
                return UnprocessableEntity(ModelState);
            }

            if (await _repository.UserIdentity.GetUserIdentityByEmailAsync(user.EmailAddress, false) != null)
            {
                _logger.LogError("Invalid model state for the Registration object");
                ModelState.AddModelError("Email", "This email already exist");
                return UnprocessableEntity(ModelState);

            }
            var userEntity = _mapper.Map<UserIdentity>(user);
            //хеширую пороль
            userEntity.Password = IdentityHelper.hashPassword(user.Password);
            //по умолчанию роль User
            userEntity.Role = "User";

            _repository.UserIdentity.CreateUserIdentity(userEntity);
            await _repository.SaveAsync();

            return Ok("Registration is successful");

        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
            {
                _logger.LogInfo(" Login object sent from client is null.");
                return BadRequest("Login object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogInfo("Invalid model state for the Login object");
                return UnprocessableEntity(ModelState);
            }

            var entityUser = await _repository.UserIdentity.GetUserIdentityByEmailAsync(userLogin.Login, trackChanges:false);
            if(entityUser == null)
            {
                _logger.LogError("Wrong Email");
                return NotFound("Wrong email");
            }
            var t = IdentityHelper.hashPassword(userLogin.Password);
            if (entityUser.Password != IdentityHelper.hashPassword(userLogin.Password))
            {
                _logger.LogError("Wrong password");
                return BadRequest("Wrong password");
            }

            var token = IdentityHelper.Generate(entityUser, _config);

            return Ok(token);
        }

        

        


    }

    
}
