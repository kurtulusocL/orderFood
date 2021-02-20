using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ComplainService
{
    public interface IComplainService
    {
        void Create(Complain model);
        void Update(Complain model);
        List<Complain> GetAllIncluding();
        Complain GetById(int? id);
        void Delete(Complain model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
