using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SubcategoryService
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }
        public void Create(Subcategory model)
        {
            _subcategoryRepository.Create(model);
        }

        public void Delete(Subcategory model)
        {
            _subcategoryRepository.Delete(model);
        }

        public List<Subcategory> GetAllIncluding()
        {
            return _subcategoryRepository.GetAllIncluding();
        }

        public Subcategory GetById(int? id)
        {
            return _subcategoryRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _subcategoryRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _subcategoryRepository.SetDeActive(id);
        }

        public void Update(Subcategory model)
        {
            _subcategoryRepository.Update(model);
        }
    }
}
