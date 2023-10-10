using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErrorModel
{
    public static  class ErrorRole
    {
        private static readonly List<string> _allowRoles = new List<string> { "Users", "Admin", "Support", "SuperAdmin" };

        public static bool CheckRole(string roleName) 
        {
            return _allowRoles.Contains(roleName);
        }

        
    }
}
