using System;
using BusinessLogicLayer;
using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("/api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;

        public CategoryController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }


        [HttpGet("{section}")]
        public IActionResult GetCategories(string section)
        {
            var categories = _categoryBLL.GetSectionCategories(section);
            return Ok(categories);
        }

    }
}
