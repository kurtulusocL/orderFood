using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ToMakeService
{
    public class ToMakeService : IToMakeService
    {
        private readonly IToMakeRepository _toMakeRepository;
        public ToMakeService(IToMakeRepository toMakeRepository)
        {
            _toMakeRepository = toMakeRepository;
        }

        public void Continue(int id)
        {
            _toMakeRepository.Continue(id);
        }

        public void Create(ToMake model)
        {
            _toMakeRepository.Create(model);
        }

        public void Delete(ToMake model)
        {
            _toMakeRepository.Delete(model);
        }

        public void Finished(int id)
        {
            _toMakeRepository.Finished(id);
        }

        public List<ToMake> GetAll()
        {
            return _toMakeRepository.GetAll();
        }

        public ToMake GetById(int? id)
        {
            return _toMakeRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _toMakeRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _toMakeRepository.SetDeActive(id);
        }

        public void Update(ToMake model)
        {
            _toMakeRepository.Update(model);
        }
    }
}
