using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class OrderCreationDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public ICollection<OrderItemsCreationDto> OrderItems { get; set; }
        [Required]
        public ShippingCreationDto Shipping { get; set; }
        [Required]
        public InvoiceCreationDto Invoice { get; set; }
    }
}
