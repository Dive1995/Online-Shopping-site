using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccessLayer.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingContext _context;

        public ProductRepository(ShoppingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges();
        }

        public ICollection<Product> GetCategoryOfProducts(int categoryId)
        {
            var products = _context.Products.Where(prod => prod.CategoryId == categoryId).Include(product => product.ProductStock).ToList();
            return products;

        }

        public ICollection<Product> GetProductsOfSection(string section)
        {
            return _context.Products.Join(_context.Categories.Where(cat => cat.Section == section), product => product.CategoryId, category => category.Id,
                (product,category) => new Product{
                    Id = product.Id,
                    Name= product.Name,
                    Image = product.Image,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    ProductStock = product.ProductStock
                }).ToList();
        }

        public Product GetSingleProduct(int id)
        {
            var product = _context.Products.Include(product => product.ProductStock).FirstOrDefault(prod => prod.Id == id); 
            return product;
        }

    }
}
