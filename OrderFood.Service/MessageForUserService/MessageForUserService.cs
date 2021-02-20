using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using OrderFood.Service.MessageForUserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.MessageForUserService
{
    public class MessageForUserService : IMessageForUserService
    {
        private readonly IMessageForUserRepository _messageForUserRepository;
        public MessageForUserService(IMessageForUserRepository messageForUserRepository)
        {
            _messageForUserRepository = messageForUserRepository;
        }

        public void Create(MessageForUser model)
        {
            _messageForUserRepository.Create(model);
        }

        public void Delete(MessageForUser model)
        {
            _messageForUserRepository.Delete(model);
        }

        public List<MessageForUser> GetAll()
        {
            return _messageForUserRepository.GetAll();
        }

        public MessageForUser GetById(int? id)
        {
            return _messageForUserRepository.GetById(id);
        }

        public MessageForUser HitRead(int? id)
        {
            return _messageForUserRepository.HitRead(id);
        }

        public void SetActive(int id)
        {
            _messageForUserRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _messageForUserRepository.SetDeActive(id);
        }

        public void Update(MessageForUser model)
        {
            _messageForUserRepository.Update(model);
        }
    }
}
