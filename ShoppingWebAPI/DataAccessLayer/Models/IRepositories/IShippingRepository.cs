using System;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface IShippingRepository
    {
        void AddShipping(int orderId, Shipping shipping);
        Shipping GetShipping(int orderId);
        void UpdateShipping(int shippingId, Shipping shipping);
    }
}
