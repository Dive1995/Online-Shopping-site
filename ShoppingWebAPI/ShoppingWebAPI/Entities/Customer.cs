using System;
namespace ShoppingWebAPI.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNum { get; set; }
        public string Address { get; set; }

    }
}
