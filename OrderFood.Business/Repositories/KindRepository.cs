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
    public class KindRepository : GenericRepository<Kind>, IKindRepository
    {
        ApplicationDbContext _db;
        public KindRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Kind> GetAllIncluding()
        {
            return _db.Set<Kind>().Include("Companies").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Kind>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Kind>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
