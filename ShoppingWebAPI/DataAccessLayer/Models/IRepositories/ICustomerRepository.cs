using System;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface ICustomerRepository
    {
        Customer GetCustomerWithEmail(string Email);
        Customer Register(Customer customer);
        Customer GetCustomerWithId(int customerId);
        void AddRefreshToken(Customer customer, RefreshToken refreshToken);
        Customer GetCustomerByRefreshToken(string refreshToken);
    }
}
