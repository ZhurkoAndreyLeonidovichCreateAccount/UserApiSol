using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;


namespace Repository
{
    public  class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters,bool trackChanges)
        {
           
           var users =  await FindAll(trackChanges).FilterUsers(userParameters.MinAge, userParameters.MaxAge)
                       .Search(userParameters).Sort(userParameters.OrderBy).Include(u => u.Roles).ToListAsync();

            return PagedList<User>.ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);

        }

        public async Task<User?> GetUserAsync(int userId, bool trackChanges)
        {
           return await FindByCondition(u => u.Id.Equals(userId), trackChanges).Include(u=>u.Roles).SingleOrDefaultAsync();
       
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

        public async Task<User?> GetUserByEmailAsync(string email, bool trackChanges)
        {
            return await FindByCondition(u => u.Email.ToLower().Equals(email.ToLower()), trackChanges).SingleOrDefaultAsync();
        }

       
    }
}
