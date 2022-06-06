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

namespace BusinessLogicLayer
{
    public class CustomerBLL
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

            var newCustomerDto = _customerMapper.Map<CustomerDto>(newCustomer);
            newCustomerDto.Token = GenerateToken(newCustomerDto);

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

            var customerDto = _customerMapper.Map<CustomerDto>(customer);

            customerDto.Token = GenerateToken(customerDto);

            return customerDto;
        }

        public string GenerateToken(CustomerDto customerDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var jwt = _appSettings.JWTkey;
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customerDto.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public int GetUserIdFromToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.JWTkey);

        //    tokenHandler.ValidateToken(token, new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ClockSkew = TimeSpan.Zero
        //    }, out SecurityToken validatedToken);

        //    var jwtToken = (JwtSecurityToken)validatedToken;
        //    var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value);

        //    return userId;
        //}

        public CustomerDto GetCustomer(int customerId)
        {
            return _customerMapper.Map<CustomerDto>(_customerRepository.GetCustomerWithId(customerId));
        }
    }
}
