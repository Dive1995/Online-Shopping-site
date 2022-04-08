using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Models
{
    public class OrderCreationDto
    {
        public int CustomerId { get; set; }
        public float Total { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemsCreationDto> OrderItems { get; set; }
        public ShippingCreationDto Shipping { get; set; }
    }
}
