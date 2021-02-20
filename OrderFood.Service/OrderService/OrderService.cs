using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void ActiveSent(int id)
        {
            _orderRepository.ActiveSent(id);
        }

        public void Create(Order model)
        {
            _orderRepository.Create(model);
        }

        public void DeActiveSent(int id)
        {
            _orderRepository.DeActiveSent(id);
        }

        public void Delete(Order model)
        {
            _orderRepository.Delete(model);
        }

        public List<Order> GetAllIncluding()
        {
            return _orderRepository.GetAllIncluding();
        }

        public Order GetById(int? id)
        {
            return _orderRepository.GetById(id);
        }

        public Order HitRead(int? id)
        {
            return _orderRepository.HitRead(id);
        }

        public void SetActive(int id)
        {
            _orderRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _orderRepository.SetDeActive(id);
        }

        public void Update(Order model)
        {
            _orderRepository.Update(model);
        }
    }
}
