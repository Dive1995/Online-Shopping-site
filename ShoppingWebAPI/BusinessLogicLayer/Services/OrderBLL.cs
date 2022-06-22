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
          
            var result = _orderMapper.Map<ICollection<OrderDto>>(orders);

            return result;
        }
    }
}
