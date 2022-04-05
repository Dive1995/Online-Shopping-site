using System;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Contexts
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shipping> Shippings { get; set; }


        public ShoppingContext(DbContextOptions options) : base(options)
        {
        }
    }
}
