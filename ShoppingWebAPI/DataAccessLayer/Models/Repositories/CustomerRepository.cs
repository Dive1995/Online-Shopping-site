using System;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShoppingContext _context;

        public CustomerRepository(ShoppingContext context)
        {
            _context = context;
        }

        public bool Login(string email, string password)
        {
            var user = _context.Customers.FirstOrDefault(login => login.Email == email && login.Password == password);

            if(user == null)
            {
                return false;
            }

            return true;
        }

        public void Register(Customer customer)
        {            
            _context.Customers.Add(customer);
        }
    }
}
