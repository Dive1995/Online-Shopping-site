using System;
namespace BusinessLogicLayer.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfStock { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }

    }
}
