using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public void Create(Company model)
        {
            _companyRepository.Create(model);
        }

        public void Delete(Company model)
        {
            _companyRepository.Delete(model);
        }

        public List<Company> GetAllIncluding()
        {
            return _companyRepository.GetAllIncluding();
        }

        public Company GetById(int? id)
        {
            return _companyRepository.GetById(id);
        }

        public Company HitRead(int? id)
        {
            return _companyRepository.HitRead(id);
        }

        public void Like(int id)
        {
            _companyRepository.Like(id);
        }

        public void SetActive(int id)
        {
            _companyRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _companyRepository.SetDeActive(id);
        }

        public void Update(Company model)
        {
            _companyRepository.Update(model);
        }
    }
}
