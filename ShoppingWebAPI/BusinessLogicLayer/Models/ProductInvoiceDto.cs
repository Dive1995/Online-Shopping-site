using System;
namespace BusinessLogicLayer.Models
{
    public class ProductInvoiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
