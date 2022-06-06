using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    public class OrderBLL : IOrderBLL
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _orderMapper;
        private readonly IMailService _mailService;

        public OrderBLL(IOrderRepository orderRepository, IMailService mailService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderMapper = mapper;
            _mailService = mailService;
        }

        public OrderDto AddNewOrder(OrderCreationDto orderCreationDto)
        {
            //var orderDetails = new Order()
            //{
            //    CustomerId = orderCreationDto.CustomerId,
            //    Email = orderCreationDto.Email,
            //    OrderDate = DateTime.Now,
            //    Invoice = _orderMapper.Map<Invoice>(orderCreationDto.Invoice),
            //    Shipping = _orderMapper.Map<Shipping>(orderCreationDto.Shipping),
            //    OrderItems = _orderMapper.Map<ICollection<OrderItems>>(orderCreationDto.OrderItems),
            //    //Shipping = {
            //    //    FirstName = orderCreationDto.Shipping.FirstName,
            //    //    LastName = orderCreationDto.Shipping.LastName,
            //    //    PhoneNum = orderCreationDto.Shipping.PhoneNum,
            //    //    Address = orderCreationDto.Shipping.Address,
            //    //    PostalCode = orderCreationDto.Shipping.PostalCode,
            //    //    Status = orderCreationDto.Shipping.Status,
            //    //    ExpectedDeliveryDate = orderCreationDto.Shipping.ExpectedDeliveryDate,
            //    //    ShippingDate = new DateTime(),
            //    //    DeliveryDate = new DateTime(),
            //    //}
            //};

            var orderDetails = _orderMapper.Map<Order>(orderCreationDto);

            var createdOrder = _orderRepository.AddOrder(orderDetails);

            var createdOrderDto = _orderMapper.Map<OrderDto>(createdOrder);

            // sending email
            _mailService.SendOrderEmailAsync(createdOrderDto);

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


            //ICollection<OrderItemsDto> newOrderItems = new List<OrderItemsDto>();

            //foreach (var orderItem in orderEntity.OrderItems)
            //{
            //    var product = _orderMapper.Map<OrderItemsDto>(_productRepository.GetSingleProduct(orderItem.ProductId));
            //    product.Quantity = orderItem.Quantity;
            //    product.Size = orderItem.Size;
            //    newOrderItems.Add(product);
            //}

            var orderDto = new OrderDto()
            {
                Id = orderEntity.Id,
                CustomerId = orderEntity.CustomerId,
                Email = orderEntity.Email,
                OrderDate = orderEntity.OrderDate,
                OrderItems = _orderMapper.Map<ICollection<OrderItemsDto>>(orderEntity.OrderItems),
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

            var orders = _orderRepository.GetAllOrders(customerId);

            //ICollection<OrderDto> allOrders = new List<OrderDto>();

            //foreach (var order in orders)
            //{
            //    var orderDto = GetSingleOrder(order.Id, userIdFromToken);
            //    allOrders.Add(orderDto);
            //}

            var result = _orderMapper.Map<ICollection<OrderDto>>(orders);

            return result;
        }
    }
}
