using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWebAPI.Entities
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        // Navigation Properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
