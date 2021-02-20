using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.AdService
{
    public interface IAdService
    {
        void Create(Ad model);
        void Update(Ad model);
        List<Ad> GetAll();
        Ad GetById(int? id);
        void Delete(Ad model);
        Ad HitRead(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
