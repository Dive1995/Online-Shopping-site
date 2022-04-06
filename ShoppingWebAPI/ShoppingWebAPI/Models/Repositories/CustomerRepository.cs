using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebAPI.Contexts;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShoppingContext _context;

        public CustomerRepository(ShoppingContext context)
        {
            _context = context;
        }

        public string Login(string email, string password)
        {
            // 1. decrypt the password from DB and match with argument
            var user = _context.Customers.FirstOrDefault(login => login.Email == email && login.Password == password);

            if(user == null)
            {
                return null;
            }

            // create JWT token
            // return JWT token
            return "Token";
        }

        public string Register(Customer customer)
        {            
            _context.Customers.Add(customer);

            // cretae JWT token
            // return JWT token
            return "Token";
        }
    }
}
