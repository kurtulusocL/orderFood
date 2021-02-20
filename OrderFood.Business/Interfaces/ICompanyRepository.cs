using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface ICompanyRepository:IGenericRepository<Company>
    {
        List<Company> GetAllIncluding();
        Company HitRead(int? id);
        void Like(int id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
