using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class CustomerCreationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password should contain atleast one Block letter, number")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
