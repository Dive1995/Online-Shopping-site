using System;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.IServices
{
    public interface ICustomerBLL
    {
        CustomerDto GetCustomer(int customerId);
        CustomerDto RegisterCustomer(CustomerCreationDto customerDetails);
        CustomerDto Login(CustomerLoginDto customerDetails);
        CustomerDto RefreshExpiredJWTtoken(string refreshToken);

    }
}
