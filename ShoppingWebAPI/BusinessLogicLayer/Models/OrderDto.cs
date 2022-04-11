using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemsDto> OrderItems { get; set; }

        // for Shipping
        public ShippingDto Shipping { get; set; }

        public InvoiceDto Invoice { get; set; }
    }
}
