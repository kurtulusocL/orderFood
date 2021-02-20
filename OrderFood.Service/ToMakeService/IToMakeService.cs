using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ToMakeService
{
    public interface IToMakeService
    {
        void Create(ToMake model);
        void Update(ToMake model);
        List<ToMake> GetAll();
        ToMake GetById(int? id);
        void Delete(ToMake model);
        void SetActive(int id);
        void SetDeActive(int id);
        void Finished(int id);
        void Continue(int id);
    }
}
