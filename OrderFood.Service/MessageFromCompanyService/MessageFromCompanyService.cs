using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.MessageFromCompanyService
{
    public class MessageFromCompanyService : IMessageFromCompanyService
    {
        private readonly IMessageFromCompanyRepository _companyMessageRepository;
        public MessageFromCompanyService(IMessageFromCompanyRepository companyMessageRepository)
        {
            _companyMessageRepository = companyMessageRepository;
        }
        public void Create(MessageFromCompany model)
        {
            _companyMessageRepository.Create(model);
        }

        public void Delete(MessageFromCompany model)
        {
            _companyMessageRepository.Delete(model);
        }

        public List<MessageFromCompany> GetAll()
        {
            return _companyMessageRepository.GetAll();
        }

        public MessageFromCompany GetById(int? id)
        {
            return _companyMessageRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _companyMessageRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _companyMessageRepository.SetDeActive(id);
        }

        public void Update(MessageFromCompany model)
        {
            _companyMessageRepository.Update(model);
        }
    }
}
