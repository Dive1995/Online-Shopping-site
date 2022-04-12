using System;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/users")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBLL _customerBLL;

        public CustomerController(CustomerBLL customerBLL)
        {
            _customerBLL = customerBLL;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterCustomer(CustomerCreationDto customerCreationDto)
        {
            var createdCustomer = _customerBLL.RegisterCustomer(customerCreationDto);
            if(createdCustomer == null)
            {
                return BadRequest(new { message = "Email address already in use" });
            }

            return Ok(new { message = "Registered Successfully !", user = createdCustomer });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var customer = _customerBLL.GetCustomer(id);
            if(customer == null)
            {
                return NotFound("Customer doesn't exist.");
            }

            // only allow the user to get his info only

            return Ok(customer);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(CustomerLoginDto customerLoginDto)
        {
            var customer = _customerBLL.Login(customerLoginDto);
            if (customer == null)
            {
                return Unauthorized("Incorrect email / password !");
            }

            return Ok(customer);
        }

    }
}
