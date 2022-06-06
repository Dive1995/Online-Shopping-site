using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogicLayer.Models;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public float Price { get; set; }
        public string Description { get; set; }
        [Required]
        public ICollection<ProductStock> ProductStock { get; set; }


        // Navigation Properties

        // for OrderItems
        //public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        // for Category
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
