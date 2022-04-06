using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingWebAPI.Contexts;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShoppingContext _context;

        public CategoryRepository(ShoppingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
    }
}
