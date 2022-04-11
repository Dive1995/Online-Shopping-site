using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Total { get; set; }
        public float Discount { get; set; }
        public string PaymentMethod { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }

        // navigation property
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
