using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using BusinessLogicLayer.IServices;
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
        private readonly IOrderBLL _orderBLL;

        public OrderController(IOrderBLL orderBLL)
        {
            _orderBLL = orderBLL;
        }


        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetSingleOrder(int id)
        {
            string name = User.Identity.Name;
            var userId = Int32.Parse(User.Identity.Name);
            
            OrderDto order = _orderBLL.GetSingleOrder(id, userId);

            if (order == null)
            {
                return NotFound(new { message = "Invalid Order Id" });
            }
            return Ok(order);
        }


        [HttpPost]
        public ActionResult<OrderDto> AddNewOrder([FromBody] OrderCreationDto orderCreationDto)
        {
            var addedOrder = _orderBLL.AddNewOrder(orderCreationDto);

            if(addedOrder == null)
            {
                return StatusCode(500, new { message = "Couldn't add your order at the moment, please try again later." });
            }

            return CreatedAtAction("GetSingleOrder", new { id = addedOrder.Id }, new { message = "Your order has been placed successfully.", order = addedOrder });
        }


        [HttpGet("user/{id}")]
        public ActionResult<ICollection<OrderDto>> GetAllOrders(int id)
        {
            var userId = Int32.Parse(User.Identity.Name);

            var orders = _orderBLL.GetAllOrders(id, userId);

            if (orders == null)
            {
                return StatusCode(403, new { message = "You are not authorized to make the request"});
            }

            if(orders.Count <= 0)
            {
                return NotFound(new { message = "You don't have any previous orders." });
            }

            return Ok(orders);
        }
    }
}
