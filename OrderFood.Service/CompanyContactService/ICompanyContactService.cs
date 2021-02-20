using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CompanyContactService
{
    public interface ICompanyContactService
    {
        void Create(CompanyContact model);
        void Update(CompanyContact model);
        List<CompanyContact> GetAllIncluding();
        CompanyContact GetById(int? id);
        void Delete(CompanyContact model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
