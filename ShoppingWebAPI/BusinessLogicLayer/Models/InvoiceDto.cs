using System;
namespace BusinessLogicLayer.Models
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string Invoice { get { return "INV" + Id; } }
        public float Total { get; set; }
        public float Discount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
