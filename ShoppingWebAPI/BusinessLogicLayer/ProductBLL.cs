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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductBLL> _logger;
        private readonly IMapper _productMapper;

        public ProductBLL(IProductRepository productRepository, ICategoryRepository categoryRepository, ILogger<ProductBLL> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;

            _productMapper = mapper;
        }


        // Get all products for a section
        public ICollection<ProductDto> GetProductSection(string section)
        {
            var allProducts = _productRepository.GetProductsOfSection(section);
            return _productMapper.Map<ICollection<ProductDto>>(allProducts);
        }


        // get all products for a specific category
        public ICollection<ProductDto> GetAllProducts(int categoryId)
        {
            _logger.LogWarning("Getting data from Server......");
            var products = _productRepository.GetCategoryOfProducts(categoryId);
            return _productMapper.Map<ICollection<ProductDto>>(products);
        }


        // Get a single product
        public ProductDto GetProduct(int id)
        {

            var product = _productRepository.GetSingleProduct(id);
            return _productMapper.Map<ProductDto>(product);
        }


        // Create a new product
        public int AddProduct(ProductCreationDto product)
        {
            var productEntity = _productMapper.Map<Product>(product);
            var changes = _productRepository.AddProduct(productEntity);
            return changes;
        }

    }
}
