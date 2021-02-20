using OrderFood.Business.Interfaces;
using OrderFood.Data.Data;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Repositories
{
    public class AdRepository : GenericRepository<Ad>, IAdRepository
    {
        ApplicationDbContext _db;
        public AdRepository()
        {
            _db = new ApplicationDbContext();
        }
        public Ad HitRead(int? id)
        {
            var hit = _db.Set<Ad>().Where(i => i.Id == id).SingleOrDefault();
            hit.Hit++;
            _db.SaveChanges();
            return hit;
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Ad>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Ad>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
