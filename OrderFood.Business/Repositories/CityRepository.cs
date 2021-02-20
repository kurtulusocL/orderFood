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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        ApplicationDbContext _db;
        public CityRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<City> GetAllIncluding()
        {
            return _db.Set<City>().Include("Companies").Include("Products").Include("Country").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<City>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<City>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
