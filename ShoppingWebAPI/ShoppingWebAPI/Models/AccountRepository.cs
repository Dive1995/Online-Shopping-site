using System;
using System.Linq;
using ShoppingWebAPI.Contexts;

namespace ShoppingWebAPI.Models
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ShoppingContext _context;

        public AccountRepository(ShoppingContext context)
        {
            _context = context;
        }

        public void Login(string email, string password)
        {
            // 1. decrypt the password from DB and match with argument
            var user = _context.Logins.FirstOrDefault(login => login.Email == email && login.Password == password);
            if(user == null)
            {
                // user doesn't exist
            }
        }

        public void Register(CustomerRegisterDto customerDetail)
        {
            throw new NotImplementedException();
        }
    }
}
