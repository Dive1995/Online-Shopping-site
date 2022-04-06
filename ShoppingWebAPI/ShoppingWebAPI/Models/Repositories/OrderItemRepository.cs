using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingWebAPI.Contexts;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public class OrderItemRepository : IOrderItemsRepository
    {
        private readonly ShoppingContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrderItemRepository(ShoppingContext context, IOrderRepository orderRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public void AddOrderItems(int orderId, ICollection<OrderItems> orderItems)
        {
            var order = _orderRepository.GetSingleOrder(orderId);
            foreach (var orderItem in orderItems)
            {
                //orderItem.OrderId = orderId;
                //_context.OrderItems.Add(orderItem);
                order.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();
        }

        public ICollection<OrderItems> GetOrderItems(int orderId)
        {
            var orderItems = _context.OrderItems.Where(order => order.Id == orderId).ToList();
            return orderItems;
        }
    }
}
