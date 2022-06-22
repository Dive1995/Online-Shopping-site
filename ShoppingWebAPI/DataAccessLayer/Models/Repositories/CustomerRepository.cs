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

        public Customer GetCustomerWithId(int customerId)
        {
            return _context.Customers.FirstOrDefault(customer => customer.Id == customerId);
        }

        public Customer GetCustomerWithEmail(string Email)
        {
            return _context.Customers.FirstOrDefault(login => login.Email == Email);
        }

        public Customer Register(Customer customer)
        {            
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public void AddRefreshToken(Customer customer, RefreshToken refreshToken)
        {
            customer.RefreshTokens.Add(refreshToken);
            _context.Update(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerByRefreshToken(string refreshToken)
        {
            return _context.Customers.SingleOrDefault(c => c.RefreshTokens.Any(t => t.Token == refreshToken));
        }
    }
}