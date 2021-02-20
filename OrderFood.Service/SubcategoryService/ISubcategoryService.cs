using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SubcategoryService
{
    public interface ISubcategoryService
    {
        void Create(Subcategory model);
        void Update(Subcategory model);
        List<Subcategory> GetAllIncluding();
        Subcategory GetById(int? id);
        void Delete(Subcategory model);      
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
