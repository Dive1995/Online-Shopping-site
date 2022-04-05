using System;
using System.Collections.Generic;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetSingleOrder(int orderId);
        ICollection<Order> GetAllOrders(int cutomerId);
    }
}
