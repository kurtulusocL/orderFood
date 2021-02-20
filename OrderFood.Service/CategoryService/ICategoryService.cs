using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CategoryService
{
    public interface ICategoryService
    {
        void Create(Category model);
        void Update(Category model);
        List<Category> GetAllIncluding();
        Category GetById(int? id);
        void Delete(Category model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
