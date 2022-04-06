using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingWebAPI.Contexts;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingContext _context;

        public OrderRepository(ShoppingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public ICollection<Order> GetAllOrders(int cutomerId)
        {
            return _context.Orders.Where(order => order.CustomerId == cutomerId).ToList();
        }

        public Order GetSingleOrder(int orderId)
        {
            return _context.Orders.FirstOrDefault(order => order.Id == orderId);
        }
    }
}
