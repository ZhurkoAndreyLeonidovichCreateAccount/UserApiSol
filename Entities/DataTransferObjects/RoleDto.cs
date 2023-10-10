using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class RoleDto
    {
        
        public int Id { get; set; }
        
        public string RoleName { get; set; } = string.Empty;
    }
}
