using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface IProductRepository
    {
        Product GetSingleProduct(int id);
        ICollection<Product> GetCategoryOfProducts(int categoryId);
        int AddProduct(Product product);
    }
}
