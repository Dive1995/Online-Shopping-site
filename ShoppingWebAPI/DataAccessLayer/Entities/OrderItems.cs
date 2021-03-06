using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class OrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Size { get; set; }

        // Navigation Properties

        // for Product
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        // for Order
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int OrderId { get; set; }

        
    }
}
