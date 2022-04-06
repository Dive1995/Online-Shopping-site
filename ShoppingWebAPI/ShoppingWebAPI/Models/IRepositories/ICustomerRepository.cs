using System;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public interface ICustomerRepository
    {
        string Login(string email, string password);
        string Register(Customer customer);
    }
}
