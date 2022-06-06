using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.IServices
{
    public interface IDeliveryBLL
    {
        ICollection<DeliveryOption> GetDeliveryOptions();
    }
}
