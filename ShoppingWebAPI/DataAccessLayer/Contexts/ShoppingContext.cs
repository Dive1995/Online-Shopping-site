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
            //modelBuilder.Entity<Customer>().HasData(
            //    new Customer()
            //    {
            //        Id = 1,
            //        Username = "Will",
            //        Email = "willsmith@gmail.com",
            //        Password = "password"
            //    },new Customer()
            //    {
            //        Id = 2,
            //        Username = "Elon",
            //        Email = "elonmsuk@gmail.com",
            //        Password = "password",
            //    }
            //    );


            //modelBuilder.Entity<Order>().HasData(
            //    new Order()
            //    {
            //        Id = 1,
            //        CustomerId = 1,
            //        OrderDate = new DateTime(),
            //    },
            //    new Order()
            //    {
            //        Id = 2,
            //        CustomerId = 1,
            //        OrderDate = new DateTime(),
            //    },
            //    new Order()
            //    {
            //        Id = 3,
            //        CustomerId = 2,
            //        OrderDate = new DateTime(),
            //    }
            //    ) ;

            //modelBuilder.Entity<OrderItems>().HasData(
            //    new OrderItems()
            //    {
            //        Id = 1,
            //        OrderId = 1,
            //        ProductId = 1,
            //        Quantity = 2
            //    },
            //    new OrderItems()
            //    {
            //        Id = 2,
            //        OrderId = 1,
            //        ProductId = 2,
            //        Quantity = 1
            //    },
            //    new OrderItems()
            //    {
            //        Id = 3,
            //        OrderId = 1,
            //        ProductId = 3,
            //        Quantity = 4
            //    },
            //    new OrderItems()
            //    {
            //        Id = 4,
            //        OrderId = 1,
            //        ProductId = 1,
            //        Quantity = 10
            //    }
            //    );

            //modelBuilder.Entity<Shipping>().HasData(
            //    new Shipping()
            //    {
            //        Id = 1,
            //        OrderId = 1,
            //        ShippingDate = new DateTime(),
            //        DeliveryDate = new DateTime(),
            //        Status = "Delivered",
            //        FirstName = "Elon",
            //        LastName = "Musk",
            //        Address = "Colombo",
            //        PostalCode = 33,
            //        PhoneNum = 0212223333,
            //    },
            //    new Shipping()
            //    {
            //        Id = 2,
            //        OrderId = 2,
            //        ShippingDate = new DateTime(),
            //        DeliveryDate = new DateTime(),
            //        Status = "Preparing",
            //        FirstName = "Will",
            //        LastName = "Smith",
            //        Address = "Galle",
            //        PostalCode = 233,
            //        PhoneNum = 0212224444,
            //    }
                
            //    );

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
