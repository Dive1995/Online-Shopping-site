using System;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;


namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductBLL _productBLL;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductBLL productBLL, ILogger<ProductController> logger)
        {
            _productBLL = productBLL;
            _logger = logger;
        }

        //[HttpGet]
        //public IActionResult Products()
        //{
        //    _logger.LogWarning("Producct page requested");

        //    throw new Exception("Page not found");

        //    _logger.LogWarning("Product Page returned");
        //    return Ok("Products Page");
        //}

        [HttpGet("{id}")]
        public IActionResult GetSingleProduct(int id)
        {
            var product = _productBLL.GetProduct(id);
            if (product == null)
            {
                return NotFound("The product you are searching for is not available.");
            }

            return Ok(product);
        }

        [HttpGet("section/{section}")]
        public IActionResult GetSectionProducts(string section)
        {
            var products = _productBLL.GetProductSection(section);
            return Ok(products);
        }


        [HttpGet("category/{id}")]
        public IActionResult GetAllProducts(int id)
        {
            var products = _productBLL.GetAllProducts(id);

            if(products.Count <= 0)
            {
                return NotFound("There are no categories of products available for your search.");
            }

            return Ok(products);
        }

        // Create new products
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreationDto product)
        {
            var result = _productBLL.AddProduct(product);
            if(result <= 0)
            {
                return StatusCode(500);
            }
            return StatusCode(201, "Product added successfully!!");
        }
    }
}
