using System;
using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.IServices
{
    public interface IOrderBLL
    {
        OrderDto AddNewOrder(OrderCreationDto order);
        OrderDto GetSingleOrder(int orderId, int userIdFromToken);
        ICollection<OrderDto> GetAllOrders(int customerId, int userIdFromToken);
    }
}
