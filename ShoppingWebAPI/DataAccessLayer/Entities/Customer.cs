using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[MaxLength(15)]
        //public string FirstName { get; set; }
        //[MaxLength(15)]
        //public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //[MaxLength(10)]
        //public int PhoneNum { get; set; }
        //[MaxLength(200)]
        //public string Address { get; set; }
        //public int PostalCode { get; set; }


        // Navigation Properties

        // for Order
        public ICollection<Order> MyProperty { get; set; } = new List<Order>();

    }
}
