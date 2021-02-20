using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.KindService
{
    public interface IKindService
    {
        void Create(Kind model);
        void Update(Kind model);
        List<Kind> GetAllIncluding();
        Kind GetById(int? id);
        void Delete(Kind model);        
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
