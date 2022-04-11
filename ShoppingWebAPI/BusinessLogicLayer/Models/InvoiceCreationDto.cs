using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class InvoiceCreationDto
    {
        [Required]
        public float Total { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}
