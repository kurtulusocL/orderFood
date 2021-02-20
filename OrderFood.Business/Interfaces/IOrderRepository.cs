using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        List<Order> GetAllIncluding();
        Order HitRead(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
        void ActiveSent(int id);
        void DeActiveSent(int id);
    }
}
