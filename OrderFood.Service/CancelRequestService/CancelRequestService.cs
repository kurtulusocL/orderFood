using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CancelRequestService
{
    public class CancelRequestService : ICancelRequestService
    {
        private readonly ICancelRequestRepository _cancelRepository;
        public CancelRequestService(ICancelRequestRepository cancelRequest)
        {
            _cancelRepository = cancelRequest;
        }
        public void Create(CancelRequest model)
        {
            _cancelRepository.Create(model);
        }

        public void Delete(CancelRequest model)
        {
            _cancelRepository.Delete(model);
        }

        public List<CancelRequest> GetAllIncluding()
        {
           return _cancelRepository.GetAllIncluding();
        }

        public CancelRequest GetById(int? id)
        {
            return _cancelRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _cancelRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _cancelRepository.SetDeAcive(id);
        }

        public void Update(CancelRequest model)
        {
            _cancelRepository.Update(model);
        }
    }
}
