using System;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public interface IShippingRepository
    {
        void AddShipping(int orderId, Shipping shipping);
        Shipping GetShipping(int orderId);
    }
}
