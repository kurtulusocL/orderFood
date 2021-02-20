using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CompanySocialMediaService
{
    public class CompanySocialMediaService : ICompanySocialMediaService
    {
        private readonly ICompanySocialMediaRepository _companySocialMediaRepository;
        public CompanySocialMediaService(ICompanySocialMediaRepository companySocialMediaRepository)
        {
            _companySocialMediaRepository = companySocialMediaRepository;
        }
        public void Create(CompanySocialMedia model)
        {
            _companySocialMediaRepository.Create(model);
        }

        public void Delete(CompanySocialMedia model)
        {
            _companySocialMediaRepository.Delete(model);
        }

        public List<CompanySocialMedia> GetAllIncluding()
        {
            return _companySocialMediaRepository.GetAllIncluding();
        }

        public CompanySocialMedia GetById(int? id)
        {
            return _companySocialMediaRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _companySocialMediaRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _companySocialMediaRepository.SetDeActive(id);
        }

        public void Update(CompanySocialMedia model)
        {
            _companySocialMediaRepository.Update(model);
        }
    }
}
