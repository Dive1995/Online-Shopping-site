using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.IServices
{
    public interface ICategoryBLL
    {
        ICollection<Category> GetSectionCategories(string section);
    }
}
