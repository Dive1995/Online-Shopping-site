using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingContext _context;

        public OrderRepository(ShoppingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public ICollection<Order> GetAllOrders(int cutomerId)
        {
            return _context.Orders.Where(order => order.CustomerId == cutomerId).ToList();
        }

        public Order GetSingleOrder(int orderId)
        {
            return _context.Orders
                .Include(order => order.Shipping).ThenInclude(shipping => shipping.DeliveryOption)
                .Include(order => order.OrderItems)
                .Include(order => order.Invoice)
                .FirstOrDefault(order => order.Id == orderId);
        }
    }
}
