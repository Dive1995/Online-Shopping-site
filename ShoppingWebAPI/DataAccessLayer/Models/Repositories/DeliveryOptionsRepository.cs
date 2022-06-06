using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Models.IRepositories;

namespace DataAccessLayer.Models.Repositories
{
    public class DeliveryOptionsRepository : IDeliveryOptionsRepository
    {
        private readonly ShoppingContext _context;

        public DeliveryOptionsRepository(ShoppingContext context)
        {
            _context = context;
        }

        public ICollection<DeliveryOption> ReturnDeliveryOptions()
        {
            return _context.DeliveryOptions.ToList();
        }
    }
}
