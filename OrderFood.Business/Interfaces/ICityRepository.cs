using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface ICityRepository:IGenericRepository<City>
    {
        List<City> GetAllIncluding();
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
