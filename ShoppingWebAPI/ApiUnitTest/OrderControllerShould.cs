using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingWebAPI.Controllers;
using Xunit;

namespace ApiUnitTest
{
    public class OrderControllerShould
    {
        private readonly OrderController _bookController;
        private readonly Mock<IOrderBLL> _orderBLLMock = new Mock<IOrderBLL>();

        public OrderControllerShould()
        {
            _bookController = new OrderController(_orderBLLMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "2"),

            }, null));
            _bookController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

        }

        // Testing Single Order requests

        [Fact]
        public void ReturnSingleOrder()
        {
            // Arrange
            int orderId = 2;            

            _orderBLLMock.Setup(x => x.GetSingleOrder(orderId, It.IsAny<int>()))
                .Returns(new OrderDto { Id = 2, CustomerId = 3,Email = "diveshan1995@gmail.com"});

            // Act
            var result = _bookController.GetSingleOrder(orderId);

            // Assert
            var order = Assert.IsType<OkObjectResult>(result.Result);

            //var order = result.Result as OkObjectResult;

            var orderItem = Assert.IsType<OrderDto>(order.Value);
            Assert.Equal(orderId, orderItem.Id);

        }

        [Fact]
        public void ReturnNullIfNoOrderExist()
        {
            // Arrange
            int orderId = 2;

            _orderBLLMock.Setup(x => x.GetSingleOrder(orderId, It.IsAny<int>()))
                .Returns(() => null);

            // Act
            var result = _bookController.GetSingleOrder(orderId);

            // Assert
            var order = Assert.IsType<NotFoundObjectResult>(result.Result);

            var message = new { message = "Invalid Order Id" };

            Assert.Equal(message.ToString(), order.Value.ToString());

        }

        // Testing all orders requests

        [Fact]
        public void ReturnAllOrders()
        {
            // Arrange
            int customerId = 2;

            _orderBLLMock.Setup(x => x.GetAllOrders(customerId, It.IsAny<int>()))
                .Returns(new List<OrderDto> { new OrderDto { Id = 1 }, new OrderDto { Id = 2 } });

            // Act
            var result = _bookController.GetAllOrders(customerId);

            // Assert
            var orderList = Assert.IsType<OkObjectResult>(result.Result);

            Assert.NotNull(orderList.Value);

            var order = orderList.Value as ICollection<OrderDto>;

            Assert.Equal(2, order.Count);
        }


        [Fact]
        public void NotFoundIfNoOrders()
        {
            // Arrange
            _orderBLLMock.Setup(x => x.GetAllOrders(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<OrderDto> {});

            var message = new { message = "You don't have any previous orders." };

            // Act
            var result = _bookController.GetAllOrders(It.IsAny<int>());

            // Assert
            var orderList = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(message.ToString(), orderList.Value.ToString());
        }


        [Fact]
        public void ForbidenForUnauthorizedReq()
        {
            // Arrange
            _orderBLLMock.Setup(x => x.GetAllOrders(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => null);

            var message = new { message = "You are not authorized to make the request" };

            // Act
            var orders = _bookController.GetAllOrders(It.IsAny<int>());

            // Assert
            var result = Assert.IsType<ObjectResult>(orders.Result);
            Assert.Equal(403, result.StatusCode);
            Assert.Equal(message.ToString(), result.Value.ToString());
        }


        // Testing add new order request
        [Fact]
        public void CreateNewOrderAndReturn()
        {
            // Arrange
            var addedOrderInBLL = new OrderDto { Id = 1, CustomerId = 1 };

            _orderBLLMock.Setup(x => x.AddNewOrder(It.IsAny<OrderCreationDto>())).Returns(addedOrderInBLL);

            var response = new { message = "Your order has been placed successfully.", order = addedOrderInBLL };

            // Act
            var order = _bookController.AddNewOrder(It.IsAny<OrderCreationDto>());

            // Assert
            var createdOrder = Assert.IsType<CreatedAtActionResult>(order.Result);
            Assert.Equal(response.ToString(), createdOrder.Value.ToString());
        }

        [Fact]
        public void NotCreateOrder()
        {
            // Arrange
            _orderBLLMock.Setup(x => x.AddNewOrder(It.IsAny<OrderCreationDto>())).Returns(() => null);

            var response = new { message = "Couldn't add your order at the moment, please try again later." };

            // Act
            var order = _bookController.AddNewOrder(It.IsAny<OrderCreationDto>());

            // Assert
            var createdOrder = Assert.IsType<ObjectResult>(order.Result);
            Assert.Equal(500, createdOrder.StatusCode);
            Assert.Equal(response.ToString(), createdOrder.Value.ToString());
        }
    }
}
