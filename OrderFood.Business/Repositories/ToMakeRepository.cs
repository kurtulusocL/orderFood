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
    public class ToMakeRepository : GenericRepository<ToMake>, IToMakeRepository
    {
        ApplicationDbContext _db;
        public ToMakeRepository()
        {
            _db = new ApplicationDbContext();
        }

        public void Continue(int id)
        {
            var finished = _db.Set<ToMake>().Where(i => i.Id == id).FirstOrDefault();
            finished.IsFinished = false;
            _db.SaveChanges();
        }

        public void Finished(int id)
        {
            var finished = _db.Set<ToMake>().Where(i => i.Id == id).FirstOrDefault();
            finished.IsFinished = true;
            _db.SaveChanges();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<ToMake>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<ToMake>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
