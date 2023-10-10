using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters, bool trackChanges);

        Task<User?> GetUserAsync(int userId, bool trackChanges);

        Task<User?> GetUserByEmailAsync(string email, bool trackChanges);

        void CreateUser(User user);

        void DeleteUser(User user);

    }
}
