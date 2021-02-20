using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface IGenericRepository<T> where T : class, new()
    {
        void Create(T model);
        void Update(T model);
        List<T> GetAll();
        T GetById(int? id);
        void Delete(T model);
    }
}
