using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(Category model)
        {
            _categoryRepository.Create(model);
        }

        public void Delete(Category model)
        {
            _categoryRepository.Delete(model);
        }

        public List<Category> GetAllIncluding()
        {
            return _categoryRepository.GetAllIncluding();
        }

        public Category GetById(int? id)
        {
            return _categoryRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _categoryRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _categoryRepository.SetDeActive(id);
        }

        public void Update(Category model)
        {
            _categoryRepository.Update(model);
        }
    }
}
