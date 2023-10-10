using Entities.DataTransferObjects.Auth;
using Entities.Models;

namespace Contracts
{
    public interface IUserIdentityRepository
    {
        Task<UserIdentity?> GetUserIdentityAsync(UserLogin login, bool trackChanges);

        Task<UserIdentity?> GetUserIdentityByEmailAsync(string email, bool trackChanges);

        public void CreateUserIdentity(UserIdentity user);
        
    }
}
