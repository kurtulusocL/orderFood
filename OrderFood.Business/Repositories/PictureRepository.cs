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
    public class PictureRepository : GenericRepository<Picture>, IPictureRepository
    {
        ApplicationDbContext _db;
        public PictureRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Picture> GetAllIncluding()
        {
            return _db.Set<Picture>().Include("Product").Include("Company").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Picture>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Picture>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
