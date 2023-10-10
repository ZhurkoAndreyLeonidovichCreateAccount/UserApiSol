using Entities.Models;

namespace Contracts
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles(bool trackChanges);

        Role? GetRoleByName(string nameRole, bool trackChanges);


    }
}
