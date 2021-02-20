using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SendMailService
{
    public interface ISendMailService
    {
        void Create(SendMail model);
        void Update(SendMail model);
        List<SendMail> GetAll();
        SendMail GetById(int? id);
        void Delete(SendMail model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
