﻿using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLogicLayer
{
    public class CategoryBLL
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBLL(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICollection<Category> GetSectionCategories(string section)
        {
            return _categoryRepository.GetCategoryForSection(section);
        }

    }
}
