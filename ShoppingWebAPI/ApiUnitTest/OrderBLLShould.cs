using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Profile;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using Moq;
using Xunit;

namespace ApiUnitTest
{
    public class OrderBLLShould
    {
        private readonly OrderBLL _orderBLL;
        private readonly Mock<IOrderRepository> _orderRepoMock = new Mock<IOrderRepository>();
        //private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();
        private readonly Mock<IMailService> _mailServiceMock = new Mock<IMailService>();
        //private readonly Mock<IMapper> _autoMapperMock = new Mock<IMapper>();
        private IMapper _mapper;

        private readonly Order expectedResult;

        public OrderBLLShould()
        {
            var myProfile = new AutoMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            _orderBLL = new OrderBLL(_orderRepoMock.Object, _mailServiceMock.Object, _mapper);

            expectedResult = new Order
            {
                Id = 1,
                Email = "diveshan1995@gmail.com",
                CustomerId = 5,
                OrderDate = Convert.ToDateTime("2022-05-23T11:32:51.990053"),
                OrderItems = new List<OrderItems> {
                        new OrderItems {
                            Id = 11,
                            Quantity = 1,
                            Size = "l",
                            ProductId = 3
                        }
                    },
                Shipping = new Shipping
                {
                    Id = 29,
                    FirstName = "diveshan",
                    LastName = "dive",
                    PhoneNum = 768642734,
                    Address = "thavady",
                    PostalCode = 9,
                    Status = "ordered",
                    ShippingDate = Convert.ToDateTime("0001-01-01T00:00:00"),
                    ExpectedDeliveryDate = Convert.ToDateTime("2022-05-29T06:02:45.982"),
                    DeliveryDate = Convert.ToDateTime("0001-01-01T00:00:00"),
                    DeliveryOption = new DeliveryOption
                    {
                        Id = 1,
                        Type = "Standard",
                        Price = 300,
                        Days = 6
                    }
                },
                Invoice = new Invoice
                {
                    Id = 27,
                    Total = 1700,
                    Discount = 0,
                    PaymentMethod = "cash on delivery",
                    PaymentDate = Convert.ToDateTime("0001-01-01T00:00:00")
                }

            };
        }

        // Testing getting single order

        [Fact]
        public void ReturnNullIfOrderDoesntExist()
        {
            // Arrange
            int falseOrderId = 3;
            int userId = 5;

            _orderRepoMock.Setup((x) => x.GetSingleOrder(falseOrderId)).Returns(() => null);

            // Act
            var order = _orderBLL.GetSingleOrder(falseOrderId, userId);

            // Assert
            Assert.Null(order);

        }

        [Fact]
        public void ReturnNullIfUserIsNotAuthorizedToGetSingleOrder()
        {
            // Arrange
            int orderId = 30;
            int userIdFromToken = 1;

            _orderRepoMock.Setup(x => x.GetSingleOrder(orderId)).Returns(expectedResult);

            // Act
            var order = _orderBLL.GetSingleOrder(orderId, userIdFromToken);

            // Assert
            Assert.Null(order);
        }


        [Fact]
        public void ReturnSingleOrder()
        {
            // Arrange
            int orderId = 1;
            int userIdFromToken = 5;

            

            _orderRepoMock.Setup((x) => x.GetSingleOrder(orderId))
                .Returns(expectedResult);

            //foreach(var prod in expectedOutput.OrderItems)
            //{
            //    _productRepoMock.Setup((x) => x.GetSingleProduct(prod.ProductId)).Returns(new Product { Id = 1, Name = "Name"});
            //}

            //_autoMapperMock.Setup(x => x.Map<OrderDto>(It.IsAny<Order>())).Returns(new OrderDto());
            //_autoMapperMock.Setup(x => x.Map<ShippingDto>(It.IsAny<Shipping>())).Returns(new ShippingDto());
            //_autoMapperMock.Setup(x => x.Map<InvoiceDto>(It.IsAny<Invoice>())).Returns(new InvoiceDto());

            // Act
            var order = _orderBLL.GetSingleOrder(orderId, userIdFromToken);

            // Assert
            Assert.NotNull(order);
            Assert.IsType<OrderDto>(order);
        }

        // Testing Get All orders
        [Fact]
        public void ReturnNullIfUserIsNotAuthorizedToGetAllOrders()
        {
            // Arrange
            int customerId = 2;
            int userIdFromToken = 4;

            // Act
            var orders = _orderBLL.GetAllOrders(customerId, userIdFromToken);

            // Assert
            Assert.Null(orders);
        }

        [Fact]
        public void ReturnAllOrders()
        {
            // Arrange
            int customerId = 3;
            int userIdFromToken = 3;

            _orderRepoMock.Setup(x => x.GetAllOrders(customerId)).Returns(new List<Order>());

            // Act
            var orders = _orderBLL.GetAllOrders(customerId, userIdFromToken);

            // Assert
            Assert.NotNull(orders);
            Assert.IsType<List<OrderDto>>(orders);
        }

        // Testing creating neew Order

        [Fact]
        public void CreateNewOrder()
        {
            // Arrange
            var newOrder = new OrderCreationDto
            {
                CustomerId = 3,
                Email = "diveshan1995@gmail.com",
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItemsCreationDto> {
                    new OrderItemsCreationDto{
                        ProductId = 9,
                        Quantity = 4,
                        Size = "s"
                    }
                },
                Shipping = new ShippingCreationDto
                {
                    FirstName = "Diveshan",
                    LastName = "Thavarasa",
                    PhoneNum = 765465743,
                    Address = "Manipay, Sri Lanka",
                    PostalCode = 300,
                    Status = "ordered",
                    ExpectedDeliveryDate = DateTime.Now,
                    DeliveryOptionId = 1
                },
                Invoice = new InvoiceCreationDto
                {
                    Total = 800,
                    PaymentMethod = "cash on delivery"
                }
            };

            _orderRepoMock.Setup(x => x.AddOrder(It.IsAny<Order>())).Returns(_mapper.Map<Order>(newOrder));

            // Act
            var createdOrder = _orderBLL.AddNewOrder(newOrder);

            // Action
            _orderRepoMock.Verify(x => x.AddOrder(It.IsAny<Order>()), Times.Once);

            Assert.NotNull(createdOrder);
            Assert.IsType<OrderDto>(createdOrder);
        }

        [Fact]
        public void SendEmailAfterSuccessfulOrder()
        {
            var newOrder = new OrderCreationDto
            {
                CustomerId = 3,
                Email = "diveshan1995@gmail.com",
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItemsCreationDto> {
                    new OrderItemsCreationDto{
                        ProductId = 9,
                        Quantity = 4,
                        Size = "s"
                    }
                },
                Shipping = new ShippingCreationDto
                {
                    FirstName = "Diveshan",
                    LastName = "Thavarasa",
                    PhoneNum = 765465743,
                    Address = "Manipay, Sri Lanka",
                    PostalCode = 300,
                    Status = "ordered",
                    ExpectedDeliveryDate = DateTime.Now,
                    DeliveryOptionId = 1
                },
                Invoice = new InvoiceCreationDto
                {
                    Total = 800,
                    PaymentMethod = "cash on delivery"
                }
            };

            _orderRepoMock.Setup(x => x.AddOrder(It.IsAny<Order>())).Returns(_mapper.Map<Order>(newOrder));
            _mailServiceMock.Setup(x => x.SendOrderEmailAsync(It.IsAny<OrderDto>())).Returns(Task.CompletedTask);

            // Act
            var createdOrder = _orderBLL.AddNewOrder(newOrder);

            // Action
            Assert.NotNull(createdOrder);
            Assert.IsType<OrderDto>(createdOrder);

            _orderRepoMock.Verify(x => x.AddOrder(It.IsAny<Order>()), Times.Once);
            _mailServiceMock.Verify(x => x.SendOrderEmailAsync(It.IsAny<OrderDto>()), Times.Once);

        }
    }

}
