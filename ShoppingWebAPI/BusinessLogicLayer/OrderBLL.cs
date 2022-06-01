using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    public class OrderBLL
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly Mapper _orderMapper;
        private readonly IMailService _mailService;

        public OrderBLL(IOrderRepository orderRepository, IProductRepository productRepository, IMailService mailService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;

            var _orderConfuguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<OrderItems, OrderItemsCreationDto>().ReverseMap();
                cfg.CreateMap<Shipping, ShippingCreationDto>().ReverseMap();
                cfg.CreateMap<Invoice, InvoiceCreationDto>().ReverseMap();
                cfg.CreateMap<Order, OrderDto>().ReverseMap();
                cfg.CreateMap<Shipping, ShippingDto>().ReverseMap();
                cfg.CreateMap<OrderItems, OrderItemsDto>().ReverseMap();
                cfg.CreateMap<Invoice, InvoiceDto>().ReverseMap();
                cfg.CreateMap<Product, OrderItemsDto>().ReverseMap();
                //cfg.CreateMap<Product, ProductInvoiceDto>().ReverseMap();       // rather than converting Product -> ProductInvoiceDto then
                //cfg.CreateMap<OrderItemsDto, ProductInvoiceDto>().ReverseMap(); // ProductInvoceDto -> OrderItemsDto make it as one step (remove ProductInvoiceDto)
            });
            _orderMapper = new Mapper(_orderConfuguration);

            _mailService = mailService;
        }

        public OrderDto AddNewOrder(OrderCreationDto orderCreationDto)
        {
            var order = orderCreationDto;
            var orderDetails = new Order()
            {
                CustomerId = orderCreationDto.CustomerId,
                Email = orderCreationDto.Email,
                OrderDate = DateTime.Now,
                Invoice = _orderMapper.Map<Invoice>(orderCreationDto.Invoice),
                Shipping = _orderMapper.Map<Shipping>(orderCreationDto.Shipping),
                OrderItems = _orderMapper.Map<ICollection<OrderItems>>(orderCreationDto.OrderItems),
                //Shipping = {
                //    FirstName = orderCreationDto.Shipping.FirstName,
                //    LastName = orderCreationDto.Shipping.LastName,
                //    PhoneNum = orderCreationDto.Shipping.PhoneNum,
                //    Address = orderCreationDto.Shipping.Address,
                //    PostalCode = orderCreationDto.Shipping.PostalCode,
                //    Status = orderCreationDto.Shipping.Status,
                //    ExpectedDeliveryDate = orderCreationDto.Shipping.ExpectedDeliveryDate,
                //    ShippingDate = new DateTime(),
                //    DeliveryDate = new DateTime(),
                //}
            };
            var createdOrder = _orderRepository.AddOrder(orderDetails);
            var createdOrderDto = _orderMapper.Map<OrderDto>(createdOrder);

            // sending email
            _mailService.SendOrderEmailAsync(orderCreationDto.Email, createdOrderDto);

            return createdOrderDto;
        }

        public OrderDto GetSingleOrder(int orderId, int userIdFromToken)
        {
            var orderEntity = _orderRepository.GetSingleOrder(orderId);

            if (orderEntity == null)
            {
                return null;
            }

            if(orderEntity.CustomerId != userIdFromToken)
            {
                return null;
            }


            ICollection<OrderItemsDto> newOrderItems = new List<OrderItemsDto>();

            foreach (var orderItem in orderEntity.OrderItems)
            {
                var product = _orderMapper.Map<OrderItemsDto>(_productRepository.GetSingleProduct(orderItem.ProductId));
                product.Quantity = orderItem.Quantity;
                product.Size = orderItem.Size;
                newOrderItems.Add(product);
            }

            var orderDto = new OrderDto()
            {
                Id = orderEntity.Id,
                Email = orderEntity.Email,
                OrderDate = orderEntity.OrderDate,
                OrderItems = newOrderItems,
                Shipping = _orderMapper.Map<ShippingDto>(orderEntity.Shipping),
                Invoice = _orderMapper.Map<InvoiceDto>(orderEntity.Invoice)
            };

            return orderDto;
        }

        public ICollection<OrderDto> GetAllOrders(int customerId, int userIdFromToken)
        {
            if(customerId != userIdFromToken)
            {
                return null;
            }

            ICollection<OrderDto> allOrders = new List<OrderDto>();

            var orders = _orderRepository.GetAllOrders(customerId);
            foreach (var order in orders)
            {
                var orderDto = GetSingleOrder(order.Id, userIdFromToken);
                allOrders.Add(orderDto);
            }

            return allOrders;
        }
    }
}
