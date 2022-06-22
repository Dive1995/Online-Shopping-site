using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Cryptography;
using BusinessLogicLayer.IServices;

namespace BusinessLogicLayer
{
    public class CustomerBLL : ICustomerBLL
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _customerMapper;

        public CustomerBLL(ICustomerRepository customerRepository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _customerMapper = mapper;
            
        }

        public CustomerDto GetCustomer(int customerId)
        {
            return _customerMapper.Map<CustomerDto>(_customerRepository.GetCustomerWithId(customerId));
        }

        

        public CustomerDto RegisterCustomer(CustomerCreationDto customerCreationDto)
        {
            var customerExist = _customerRepository.GetCustomerWithEmail(customerCreationDto.Email);
            if (customerExist != null)
            {
                return null;
            }

            var customerEntity = new Customer()
            {
                Username = customerCreationDto.Username,
                Email = customerCreationDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(customerCreationDto.Password)
            };
            var newCustomer = _customerRepository.Register(customerEntity);

            var newCustomerDto = GenerateCustomerWithRefreshToken(newCustomer);

            return newCustomerDto;
        }



        public CustomerDto Login(CustomerLoginDto customerLoginDto)
        {            
            var customer = _customerRepository.GetCustomerWithEmail(customerLoginDto.Email);

            if(customer == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(customerLoginDto.Password, customer.Password);

            if (!isValidPassword)
            {
                return null;
            }

            var customerDto = GenerateCustomerWithRefreshToken(customer);
                       
            return customerDto;
        }



        private CustomerDto GenerateCustomerWithRefreshToken(Customer customer)
        {
            var customerDto = new CustomerDto();

            customerDto.Id = customer.Id;
            customerDto.IsAuthenticated = true;
            customerDto.Token = GenerateToken(customer);
            customerDto.Email = customer.Email;
            customerDto.UserName = customer.Username;

            var refreshToken = CreateRefreshToken();
            customerDto.RefreshToken = refreshToken.Token;
            customerDto.RefreshTokenExpiration = refreshToken.Expires;

            _customerRepository.AddRefreshToken(customer, refreshToken);

            return customerDto;
        }


        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }


        private string GenerateToken(Customer customerDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.JWTkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customerDto.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public CustomerDto RefreshExpiredJWTtoken(string refreshTokenToRenew)
        {
            var customerDto = new CustomerDto();

            // get user that have the refresh token
            var user = _customerRepository.GetCustomerByRefreshToken(refreshTokenToRenew);
            
            if(user == null)
            {
                customerDto.IsAuthenticated = false;
                customerDto.Message = "No user with the token found";
                return customerDto;
            }

            // get the refreshtoken details 
            var refreshToken = user.RefreshTokens.Single(r => r.Token == refreshTokenToRenew);

            // if refresh token is not active (revoked && expired)
            if (!refreshToken.IsActive)
            {
                customerDto.IsAuthenticated = false;
                customerDto.Message = "Token is not active";
                return customerDto;
            }

            // revoking the current refresh token
            refreshToken.Revoked = DateTime.Now;

            // creating new refresh token
            var newRefreshToken = CreateRefreshToken();
            _customerRepository.AddRefreshToken(user, newRefreshToken);

            customerDto.IsAuthenticated = true;
            customerDto.Token = GenerateToken(user);
            customerDto.Email = user.Email;
            customerDto.Id = user.Id;
            customerDto.UserName = user.Username;
            customerDto.RefreshToken = newRefreshToken.Token;
            customerDto.RefreshTokenExpiration = refreshToken.Expires;
            return customerDto;
        }


        //public CustomerDto RegisterCustomer(CustomerCreationDto customerCreationDto)
        //{
        //    var customerExist = _customerRepository.GetCustomerWithEmail(customerCreationDto.Email);
        //    if (customerExist != null)
        //    {
        //        return null; 
        //    }

        //    var customerEntity = new Customer()
        //    {
        //        Username = customerCreationDto.Username,
        //        Email = customerCreationDto.Email,
        //        Password = BCrypt.Net.BCrypt.HashPassword(customerCreationDto.Password)
        //    };
        //    var newCustomer = _customerRepository.Register(customerEntity);

        //    var newCustomerDto = _customerMapper.Map<CustomerDto>(newCustomer);
        //    newCustomerDto.Token = GenerateToken(newCustomer);

        //    return newCustomerDto;
        //}

        //public CustomerDto Login(CustomerLoginDto customerLoginDto)
        //{
        //    var customer = _customerRepository.GetCustomerWithEmail(customerLoginDto.Email);

        //    if(customer == null)
        //    {
        //        return null;
        //    }

        //    bool isValidPassword = BCrypt.Net.BCrypt.Verify(customerLoginDto.Password, customer.Password);

        //    if (!isValidPassword)
        //    {
        //        return null;
        //    }

        //    var customerDto = _customerMapper.Map<CustomerDto>(customer);

        //    customerDto.Token = GenerateToken(customerDto);

        //    return customerDto;
        //}


    }
}
