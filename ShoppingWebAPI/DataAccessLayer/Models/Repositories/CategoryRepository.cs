using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
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

        public ICollection<Category> GetCategoryForSection(string section)
        {
            return _context.Categories.Where(category => category.Section.ToLower() == section.ToLower()).ToList();
        }
    }
}
