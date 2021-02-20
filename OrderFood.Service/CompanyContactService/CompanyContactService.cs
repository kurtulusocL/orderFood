using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CompanyContactService
{
    public class CompanyContactService : ICompanyContactService
    {
        private readonly ICompanyContactRepository _companyContactRepository;
        public CompanyContactService(ICompanyContactRepository companyContactRepository)
        {
            _companyContactRepository = companyContactRepository;
        }
        public void Create(CompanyContact model)
        {
            _companyContactRepository.Create(model);
        }

        public void Delete(CompanyContact model)
        {
            _companyContactRepository.Delete(model);
        }

        public List<CompanyContact> GetAllIncluding()
        {
            return _companyContactRepository.GetAllIncluding();
        }

        public CompanyContact GetById(int? id)
        {
            return _companyContactRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _companyContactRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _companyContactRepository.SetDeActive(id);
        }

        public void Update(CompanyContact model)
        {
            _companyContactRepository.Update(model);
        }
    }
}
