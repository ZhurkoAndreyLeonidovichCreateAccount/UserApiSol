using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Auth
{
    public class UserReg
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string LastName { get; set; } = string.Empty;

        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
       
    }
}
