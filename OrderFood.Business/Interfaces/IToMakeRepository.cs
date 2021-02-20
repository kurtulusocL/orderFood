using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface IToMakeRepository:IGenericRepository<ToMake>
    {
        void SetActive(int id);
        void SetDeActive(int id);
        void Finished(int id);
        void Continue(int id);
    }
}
