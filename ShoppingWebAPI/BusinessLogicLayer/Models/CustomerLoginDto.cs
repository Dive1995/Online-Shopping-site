using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class CustomerLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
