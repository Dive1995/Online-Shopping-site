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

        //public Customer CustomerExist(string email)
        //{
        //    return _context.Customers.FirstOrDefault(customer => customer.Email == email);
        //}
    }
}
