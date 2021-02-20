using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public void Create(Payment model)
        {
            _paymentRepository.Create(model);
        }

        public void Delete(Payment model)
        {
            _paymentRepository.Delete(model);
        }

        public List<Payment> GetAllIncluding()
        {
            return _paymentRepository.GetAllIncluding();
        }

        public Payment GetById(int? id)
        {
            return _paymentRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _paymentRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _paymentRepository.SetDeActive(id);
        }

        public void Update(Payment model)
        {
            _paymentRepository.Update(model);
        }
    }
}
