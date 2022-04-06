using System;
using Microsoft.AspNetCore.Mvc;
using ShoppingWebAPI.Models;

namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Products Page");
        }

        [HttpGet("category/{id}")]
        public IActionResult GetAllProducts(int id)
        {
            var products = _productRepository.GetCategoryOfProducts(id);

            if(products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleProduct(int id)
        {
            var product = _productRepository.GetSingleProduct(id);
            if(product == null)
            {
                return BadRequest();
            }

            return Ok(product);
        }

        //[HttpPost]
        //public IActionResult AddProduct()
        //{

        //}
    }
}
