using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ComplainService
{
    public class ComplainService : IComplainService
    {
        private readonly IComplainRepository _complainRepository;
        public ComplainService(IComplainRepository complainRepository)
        {
            _complainRepository = complainRepository;
        }
        public void Create(Complain model)
        {
            _complainRepository.Create(model);
        }

        public void Delete(Complain model)
        {
            _complainRepository.Delete(model);
        }

        public List<Complain> GetAllIncluding()
        {
            return _complainRepository.GetAllIncluding();
        }

        public Complain GetById(int? id)
        {
            return _complainRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _complainRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _complainRepository.SetDeActive(id);
        }

        public void Update(Complain model)
        {
            _complainRepository.Update(model);
        }
    }
}
