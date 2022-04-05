using System;
using System.Collections.Generic;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public interface IProductRepository
    {
        Product GetSingleProduct(int id);
        ICollection<Product> GetCategoryOfProducts(int categoryId);
        void AddProduct(Product product);
    }
}
