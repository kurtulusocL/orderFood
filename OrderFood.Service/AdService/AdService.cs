using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.AdService
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;
        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }
        public void Create(Ad model)
        {
            _adRepository.Create(model);
        }

        public void Delete(Ad model)
        {
            _adRepository.Delete(model);
        }

        public List<Ad> GetAll()
        {
            return _adRepository.GetAll();
        }

        public Ad GetById(int? id)
        {
            return _adRepository.GetById(id);
        }

        public Ad HitRead(int? id)
        {
            return _adRepository.HitRead(id);
        }

        public void SetActive(int id)
        {
            _adRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _adRepository.SetDeActive(id);
        }

        public void Update(Ad model)
        {
            _adRepository.Update(model);
        }
    }
}
