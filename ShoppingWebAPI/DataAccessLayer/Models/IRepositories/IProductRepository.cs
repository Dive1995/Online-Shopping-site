using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface IProductRepository
    {
        int AddProduct(Product product);
        Product GetSingleProduct(int id);
        ICollection<Product> GetProductsOfSection(string section);
        ICollection<Product> GetCategoryOfProducts(int categoryId);
    }
}
