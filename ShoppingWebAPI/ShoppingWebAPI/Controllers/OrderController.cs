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
    [Route("/api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderBLL _orderBLL;

        public OrderController(OrderBLL orderBLL)
        {
            _orderBLL = orderBLL;
        }

        [HttpGet]
        public IActionResult Order()
        {
            return Ok("Orders");
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleOrder(int id)
        {
            return Ok(_orderBLL.GetSingleOrder(id));
        }

        [HttpPost]
        public IActionResult AddNewOrder([FromBody] OrderCreationDto orderCreationDto)
        {
            var addedOrder = _orderBLL.AddNewOrder(orderCreationDto);

            if(addedOrder <= 0)
            {
                return StatusCode(500, "Couldn't add your order at the moment, please try again.");
            }

            return StatusCode(201, "Order Successfully added.");
        }

        [HttpGet("user/{id}")]
        public IActionResult GetAllOrders(int id)
        {
            var orders = _orderBLL.GetAllOrders(id);
            if(orders.Count <= 0)
            {
                return NotFound("You don't have any previous orders.");
            }

            return Ok(orders);
        }
    }
}
