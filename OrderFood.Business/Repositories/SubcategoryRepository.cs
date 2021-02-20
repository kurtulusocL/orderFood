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
    public class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
    {
        ApplicationDbContext _db;
        public SubcategoryRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Subcategory> GetAllIncluding()
        {
            return _db.Set<Subcategory>().Include("Category").Include("Products").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Subcategory>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Subcategory>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
