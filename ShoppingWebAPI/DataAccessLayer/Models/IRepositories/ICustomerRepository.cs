using System;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface ICustomerRepository
    {
        Customer Login(Customer customer);
        Customer Register(Customer customer);
        Customer GetCustomer(int customerId);
        Customer CustomerExist(string email);
    }
}
