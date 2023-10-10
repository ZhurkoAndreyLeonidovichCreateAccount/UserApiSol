using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserIdentity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public  string Password { get; set; } = string.Empty;
       
        [Required]
        public  string Role { get; set; } = string.Empty;
       

    }
}
