using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.OrderService
{
    public interface IOrderService
    {
        void Create(Order model);
        void Update(Order model);
        List<Order> GetAllIncluding();
        Order GetById(int? id);
        void Delete(Order model);
        Order HitRead(int? id);       
        void SetActive(int id);
        void SetDeActive(int id);
        void ActiveSent(int id);
        void DeActiveSent(int id);
    }
}
