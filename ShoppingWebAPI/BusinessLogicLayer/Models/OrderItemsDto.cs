using System;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Models
{
    public class OrderItemsDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        //public string Name { get; set; }
        //public string Image { get; set; }
        //public float Price { get; set; }
        public Product Product { get; set; }

    }
}
