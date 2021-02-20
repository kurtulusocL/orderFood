using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.MessageFromCompanyService
{
    public interface IMessageFromCompanyService
    {
        List<MessageFromCompany> GetAll();
        MessageFromCompany GetById(int? id);
        void Create(MessageFromCompany model);
        void Update(MessageFromCompany model);
        void Delete(MessageFromCompany model);      
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
