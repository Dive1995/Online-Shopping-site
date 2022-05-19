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
        private readonly IProductRepository _productRepository;
        private readonly Mapper _orderMapper;

        public OrderBLL(IOrderRepository orderRepository, IProductRepository productRepository)
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
        }

        public OrderDto AddNewOrder(OrderCreationDto orderCreationDto)
        {
            var orderDetails = new Order()
            {
                CustomerId = orderCreationDto.CustomerId,
                OrderDate = DateTime.Now,
                Invoice = _orderMapper.Map<Invoice>(orderCreationDto.Invoice),
                Shipping = _orderMapper.Map<Shipping>(orderCreationDto.Shipping),
                OrderItems = _orderMapper.Map<ICollection<OrderItems>>(orderCreationDto.OrderItems)
            };
            //var orderEntity = _orderMapper.Map<Order>(orderCreationDto);
            return _orderMapper.Map<OrderDto>(_orderRepository.AddOrder(orderDetails));
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

            foreach(var orderItem in orderEntity.OrderItems)
            {
                var product = _orderMapper.Map<OrderItemsDto>(_productRepository.GetSingleProduct(orderItem.ProductId));
                product.Quantity = orderItem.Quantity;
                newOrderItems.Add(product);
            }

            var orderDto = new OrderDto()
            {
                Id = orderEntity.Id,
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
