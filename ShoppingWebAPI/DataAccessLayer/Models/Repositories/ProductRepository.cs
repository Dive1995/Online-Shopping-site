using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

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
