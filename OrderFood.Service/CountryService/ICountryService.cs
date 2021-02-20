using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CountryService
{
    public interface ICountryService
    {
        void Create(Country model);
        void Update(Country model);
        List<Country> GetAllIncluding();
        Country GetById(int? id);
        void Delete(Country model);       
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
