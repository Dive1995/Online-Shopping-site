using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class ShippingCreationDto
    {
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }
        [Required]
        public int PhoneNum { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        public int PostalCode { get; set; }
        public string Status { get; set; }
        public int OrderId { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        [Required]
        public int DeliveryOptionId { get; set; }
    }
}
