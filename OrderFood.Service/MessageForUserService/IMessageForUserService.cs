using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.MessageForUserService
{
    public interface IMessageForUserService
    {
        void Create(MessageForUser model);
        void Update(MessageForUser model);
        List<MessageForUser> GetAll();
        MessageForUser GetById(int? id);
        void Delete(MessageForUser model);
        MessageForUser HitRead(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
