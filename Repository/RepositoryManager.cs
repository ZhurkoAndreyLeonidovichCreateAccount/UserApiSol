﻿using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IUserRepository? _userRepository;
        private IRoleRepository? _roleRepository;
        private IUserIdentityRepository? _identityRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);
                return _userRepository;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new RoleRepository(_repositoryContext);
                return _roleRepository;
            }
        }

        public IUserIdentityRepository UserIdentity
        {
            get
            {
                if (_identityRepository == null)
                    _identityRepository = new UserIdentityRepository(_repositoryContext);
                return _identityRepository;
            }
        }

        

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }

}
