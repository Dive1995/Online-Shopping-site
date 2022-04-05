using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingWebAPI.Contexts;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingContext _context;

        public ProductRepository(ShoppingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public ICollection<Product> GetCategoryOfProducts(int categoryId)
        {
            var products = _context.Products.Where(prod => prod.CategoryId == categoryId).ToList();

            return products;

        }

        public Product GetSingleProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(prod => prod.Id == id);
            return product;
        }
    }
}
