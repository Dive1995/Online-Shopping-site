using System;
namespace ShoppingWebAPI.Models
{
    public interface IAccountRepository
    {
        void Login(string email, string password);
        void Register(CustomerRegisterDto customerDetail);
    }
}
