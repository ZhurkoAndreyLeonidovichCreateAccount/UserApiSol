using Contracts;
using Entities;
using Entities.DataTransferObjects.Auth;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserIdentityRepository : RepositoryBase<UserIdentity>, IUserIdentityRepository
    {

        public UserIdentityRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
        public Task<UserIdentity?> GetUserIdentityAsync(UserLogin login, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<UserIdentity?> GetUserIdentityByEmailAsync(string email, bool trackChanges)
        {
            return await FindByCondition(u => u.EmailAddress.ToLower().Equals(email.ToLower()), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateUserIdentity(UserIdentity user) => Create(user);
    }
}
