using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class ProductCreationDto
    {
        [Required(ErrorMessage = "Product Name is Required.")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public int NumOfStock { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
