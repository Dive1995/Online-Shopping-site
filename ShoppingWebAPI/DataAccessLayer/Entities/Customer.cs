using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Customer // : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }     


        // Navigation Properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public List<RefreshToken> RefreshTokens { get; set; }
        //public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
