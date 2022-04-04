using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebAPI.Models
{
    public class CustomerRegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
