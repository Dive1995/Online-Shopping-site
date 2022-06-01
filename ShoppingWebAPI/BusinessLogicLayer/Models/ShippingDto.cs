using System;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Models
{
    public class ShippingDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNum { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string Status { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryOption DeliveryOption { get; set; }

    }
}
