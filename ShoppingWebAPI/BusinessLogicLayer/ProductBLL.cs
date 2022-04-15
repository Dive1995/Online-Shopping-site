using System;
using System.Collections.Generic;
using AutoMapper;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Models.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer
{
    public class ProductBLL
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductBLL> _logger;
        private readonly Mapper _productMapper;

        public ProductBLL(IProductRepository productRepository, ILogger<ProductBLL> logger)
        {
            _productRepository = productRepository;
            _logger = logger;

            var _configProduct = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
                cfg.CreateMap<Product, ProductCreationDto>().ReverseMap();
                });
            _productMapper = new Mapper(_configProduct);
        }

        public ICollection<ProductDto> GetAllProducts(int categoryId)
        {
            _logger.LogWarning("Getting data from Server......");
            var products = _productRepository.GetCategoryOfProducts(categoryId);
            return _productMapper.Map<ICollection<ProductDto>>(products);
        }

        public ProductDto GetProduct(int id)
        {

            var product = _productRepository.GetSingleProduct(id);
            return _productMapper.Map<ProductDto>(product);
        }

        public int AddProduct(ProductCreationDto product)
        {
            var productEntity = _productMapper.Map<Product>(product);
            var changes = _productRepository.AddProduct(productEntity);
            return changes;
        }

    }
}
