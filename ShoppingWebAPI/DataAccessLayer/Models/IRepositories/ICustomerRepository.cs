using System;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface ICustomerRepository
    {
        Customer GetCustomerWithEmail(string Email);
        Customer Register(Customer customer);
        Customer GetCustomerWithId(int customerId);
        //Customer CustomerExist(string email);
    }
}
