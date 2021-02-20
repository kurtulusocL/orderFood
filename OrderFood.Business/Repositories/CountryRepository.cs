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
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        ApplicationDbContext _db;
        public CountryRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Country> GetAllIncluding()
        {
            return _db.Set<Country>().Include("Cities").Include("Companies").Include("Products").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Country>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Country>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
