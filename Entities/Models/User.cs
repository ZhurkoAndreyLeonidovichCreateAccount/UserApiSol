using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(18, 120, ErrorMessage = "Age can only be between 18 .. 120")]
        public int Age { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        public List<Role>? Roles { get; set; }
    }
}
