using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Shipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(15)]
        public string FirstName { get; set; }
        [MaxLength(15)]
        public string LastName { get; set; }
        public int PhoneNum { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string Status { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public DateTime DeliveryDate { get; set; }


        // Navigation Properties

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("DeliveryOptionId")]
        public DeliveryOption DeliveryOption { get; set; }
        public int DeliveryOptionId { get; set; }

    }
}