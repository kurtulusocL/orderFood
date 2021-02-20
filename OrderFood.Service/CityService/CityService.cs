using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CityService
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public void Create(City model)
        {
            _cityRepository.Create(model);
        }

        public void Delete(City model)
        {
            _cityRepository.Delete(model);
        }

        public List<City> GetAllIncluding()
        {
           return _cityRepository.GetAllIncluding();
        }

        public City GetById(int? id)
        {
            return _cityRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _cityRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _cityRepository.SetDeActive(id);
        }

        public void Update(City model)
        {
            _cityRepository.Update(model);
        }
    }
}
