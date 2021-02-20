using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public void Create(Country model)
        {
            _countryRepository.Create(model);
        }

        public void Delete(Country model)
        {
            _countryRepository.Delete(model);
        }

        public List<Country> GetAllIncluding()
        {
            return _countryRepository.GetAllIncluding();
        }

        public Country GetById(int? id)
        {
           return _countryRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _countryRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _countryRepository.SetDeActive(id);
        }

        public void Update(Country model)
        {
            _countryRepository.Update(model);
        }
    }
}
