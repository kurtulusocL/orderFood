using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.KindService
{
    public class KindService : IKindService
    {
        private readonly IKindRepository _kindRepository;
        public KindService(IKindRepository kindRepository)
        {
            _kindRepository = kindRepository;
        }
        public void Create(Kind model)
        {
            _kindRepository.Create(model);
        }

        public void Delete(Kind model)
        {
            _kindRepository.Delete(model);
        }

        public List<Kind> GetAllIncluding()
        {
            return _kindRepository.GetAllIncluding();
        }

        public Kind GetById(int? id)
        {
            return _kindRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _kindRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _kindRepository.SetDeActive(id);
        }

        public void Update(Kind model)
        {
            _kindRepository.Update(model);
        }
    }
}
