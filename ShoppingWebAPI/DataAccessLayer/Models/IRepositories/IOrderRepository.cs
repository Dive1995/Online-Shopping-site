﻿using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        Order GetSingleOrder(int orderId);
        ICollection<Order> GetAllOrders(int cutomerId);
    }
}
