using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Category> GetCategoryForSection(string section);
    }
}
