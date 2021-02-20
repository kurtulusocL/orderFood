using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SendMailService
{
    public class SendMailService : ISendMailService
    {
        private readonly ISendMailRepository _sendMailRepository;
        public SendMailService(ISendMailRepository sendMailRepository)
        {
            _sendMailRepository = sendMailRepository;
        }
        public void Create(SendMail model)
        {
            _sendMailRepository.Create(model);
        }

        public void Delete(SendMail model)
        {
            _sendMailRepository.Delete(model);
        }

        public List<SendMail> GetAll()
        {
            return _sendMailRepository.GetAll();
        }

        public SendMail GetById(int? id)
        {
            return _sendMailRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _sendMailRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _sendMailRepository.SetDeActive(id);
        }

        public void Update(SendMail model)
        {
            _sendMailRepository.Update(model);
        }
    }
}
