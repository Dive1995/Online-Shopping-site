using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface IOrderItemsRepository
    {
        void AddOrderItems(int orderId, ICollection<OrderItems> orderItems);
        ICollection<OrderItems> GetOrderItems(int orderId);
    }
}
