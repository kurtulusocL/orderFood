using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface ICancelRequestRepository:IGenericRepository<CancelRequest>
    {
        List<CancelRequest> GetAllIncluding();
        void SetActive(int id);
        void SetDeAcive(int id);
    }
}
