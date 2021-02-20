using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CancelRequestService
{
    public interface ICancelRequestService
    {
        void Create(CancelRequest model);
        void Update(CancelRequest model);
        List<CancelRequest> GetAllIncluding();
        CancelRequest GetById(int? id);
        void Delete(CancelRequest model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
