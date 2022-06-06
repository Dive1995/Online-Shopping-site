using System;
using System.Collections.Generic;
using BusinessLogicLayer.IServices;
using DataAccessLayer.Entities;
using DataAccessLayer.Models.IRepositories;

namespace BusinessLogicLayer.Services
{
    public class DeliveryBLL : IDeliveryBLL
    {
        private readonly IDeliveryOptionsRepository _deliveryDA;

        public DeliveryBLL(IDeliveryOptionsRepository deliveryOptionsRepository)
        {
            _deliveryDA = deliveryOptionsRepository;
        }

        public ICollection<DeliveryOption> GetDeliveryOptions()
        {
            return _deliveryDA.ReturnDeliveryOptions();
        }
    }
}
