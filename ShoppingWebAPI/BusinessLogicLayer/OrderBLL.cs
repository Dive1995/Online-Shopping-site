using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    public class OrderBLL
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Mapper _orderMapper;

        public OrderBLL(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            var _orderConfuguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<OrderItems, OrderItemsCreationDto>().ReverseMap();
                cfg.CreateMap<Shipping, ShippingCreationDto>().ReverseMap();
            });
            _orderMapper = new Mapper(_orderConfuguration);
        }

        public int AddNewOrder(OrderCreationDto orderCreationDto)
        {
            var orderDetails = new Order()
            {
                CustomerId = orderCreationDto.CustomerId,
                OrderDate = DateTime.Now,
                Total = orderCreationDto.Total,
                Shipping = _orderMapper.Map<Shipping>(orderCreationDto.Shipping),
                OrderItems = _orderMapper.Map<ICollection<OrderItems>>(orderCreationDto.OrderItems)
            };
            //var orderEntity = _orderMapper.Map<Order>(orderCreationDto);
            return _orderRepository.AddOrder(orderDetails);
        }
    }
}
