using System;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly ShoppingContext _context;
        private readonly IOrderRepository _orderRepository;

        public ShippingRepository(ShoppingContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        public void AddShipping(int orderId, Shipping shipping)
        {
            var order = _orderRepository.GetSingleOrder(orderId);
            order.Shipping = shipping;
            _context.SaveChanges();
        }

        public Shipping GetShipping(int orderId)
        {
            return _context.Shippings.FirstOrDefault(shipping => shipping.OrderId == orderId);
        }
    }
}
