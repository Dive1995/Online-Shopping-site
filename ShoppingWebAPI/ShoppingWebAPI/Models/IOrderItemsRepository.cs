using System;
using System.Collections.Generic;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public interface IOrderItemsRepository
    {
        void AddOrderItems(int orderId, ICollection<OrderItems> orderItems);
        ICollection<OrderItems> GetOrderItems(int orderId);
    }
}
