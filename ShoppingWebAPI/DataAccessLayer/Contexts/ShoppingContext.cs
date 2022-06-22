using System;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Contexts
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<DeliveryOption> DeliveryOptions { get; set; }


        public ShoppingContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<Category>().HasData(
                new Category() {
                    Id = 1,
                    Name = "Jeans",
                    Section = "Men"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Shirt",
                    Section = "Men"
                }
                ) ;

            base.OnModelCreating(modelBuilder);
        }

    }
}
