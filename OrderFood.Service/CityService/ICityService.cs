using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CityService
{
    public interface ICityService
    {
        void Create(City model);
        void Update(City model);
        List<City> GetAllIncluding();
        City GetById(int? id);
        void Delete(City model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
