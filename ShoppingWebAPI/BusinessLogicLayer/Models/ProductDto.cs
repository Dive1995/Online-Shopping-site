using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public ICollection<ProductStock> ProductStock { get; set; }
    }
}
