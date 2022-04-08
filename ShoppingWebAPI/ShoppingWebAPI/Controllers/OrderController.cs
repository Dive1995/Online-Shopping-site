using System;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWebAPI.Controllers
{
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

    }
}
