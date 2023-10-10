using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.DataTransferObjects
{
    public class UserForCreationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(18, 120, ErrorMessage = "Age can only be between 18 .. 120")]
        public int Age { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
