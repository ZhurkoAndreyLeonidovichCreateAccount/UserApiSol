using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public  class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }

        public IEnumerable<Role> GetAllRoles(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(r=>r.RoleName).ToList();
        }

        public Role? GetRoleByName(string nameRole, bool trackChanges)
        {
            return FindByCondition(r => r.RoleName.ToLower().Equals(nameRole.ToLower()), trackChanges).SingleOrDefault();
        }

       
    }
}
