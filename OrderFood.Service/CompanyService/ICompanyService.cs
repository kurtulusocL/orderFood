using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CompanyService
{
    public interface ICompanyService
    {
        void Create(Company model);
        void Update(Company model);
        List<Company> GetAllIncluding();
        Company GetById(int? id);
        void Delete(Company model);
        Company HitRead(int? id);
        void Like(int id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
