using System;
using System.Collections.Generic;
using ShoppingWebAPI.Entities;

namespace ShoppingWebAPI.Models
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
    }
}
