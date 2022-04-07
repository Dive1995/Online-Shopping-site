using System;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface ICustomerRepository
    {
        bool Login(string email, string password);
        void Register(Customer customer);
    }
}
