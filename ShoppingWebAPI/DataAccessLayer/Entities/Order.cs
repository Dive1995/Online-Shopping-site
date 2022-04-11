using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation Properties

        // for Customer 
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        // for OrderItem
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        // for Shipping
        public Shipping Shipping { get; set; }

        public Invoice Invoice { get; set; }
    }
}