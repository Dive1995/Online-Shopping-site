using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models.IRepositories
{
    public interface IDeliveryOptionsRepository
    {
        ICollection<DeliveryOption> ReturnDeliveryOptions();
    }
}
