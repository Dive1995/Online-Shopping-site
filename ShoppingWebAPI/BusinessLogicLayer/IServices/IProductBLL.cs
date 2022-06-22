using System;
using System.Collections.Generic;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Models.Dtos;

namespace BusinessLogicLayer.IServices
{
    public interface IProductBLL
    {
        ICollection<ProductDto> GetProductSection(string section);
        ICollection<ProductDto> GetAllProducts(int categoryId);
        ProductDto GetProduct(int id);
        int AddProduct(ProductCreationDto product);
    }
}
