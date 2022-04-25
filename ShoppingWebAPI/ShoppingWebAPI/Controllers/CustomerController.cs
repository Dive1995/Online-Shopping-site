using System;
using System.Linq;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ShoppingWebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/users")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBLL _customerBLL;
        private readonly ILogger _logger;

        public CustomerController(CustomerBLL customerBLL, ILogger<CustomerController> logger)
        {
            _customerBLL = customerBLL;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterCustomer(CustomerCreationDto customerCreationDto)
        {
            _logger.LogWarning("new customer registration requested");
            var createdCustomer = _customerBLL.RegisterCustomer(customerCreationDto);

            if(createdCustomer == null)
            {
                _logger.LogError("Email already in use");
                return BadRequest(new { message = "Email address already in use" });
            }

            _logger.LogWarning("new customer created");
            return Ok(new { message = "Registered Successfully !", user = createdCustomer });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            _logger.LogInformation("user information requested");

            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var customer = _customerBLL.GetCustomer(id);
            if(customer == null)
            {
                _logger.LogError("No customer with given id found");
                return NotFound("Customer doesn't exist.");
            }

            _logger.LogInformation("Returning customer details");
            return Ok(customer);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(CustomerLoginDto customerLoginDto)
        {
            _logger.LogInformation("Trying to login");
            var customer = _customerBLL.Login(customerLoginDto);

            if (customer == null)
            {
                _logger.LogError("Invalid username or password");
                return Unauthorized("Incorrect email / password !");
            }

            _logger.LogInformation("user logged in $ returned user details");
            return Ok(customer);
        }

    }
}
