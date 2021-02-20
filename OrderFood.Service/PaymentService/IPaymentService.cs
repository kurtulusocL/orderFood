using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.PaymentService
{
    public interface IPaymentService
    {
        void Create(Payment model);
        void Update(Payment model);
        List<Payment> GetAllIncluding();
        Payment GetById(int? id);
        void Delete(Payment model);               
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
