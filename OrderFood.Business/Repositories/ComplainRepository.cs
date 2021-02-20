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
    public class ComplainRepository : GenericRepository<Complain>, IComplainRepository
    {
        ApplicationDbContext _db;
        public ComplainRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Complain> GetAllIncluding()
        {
            return _db.Set<Complain>().Include("Product").Include("Company").Include("Product.City").Include("Product.Country").Include("Company.City").Include("Company.Country").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Complain>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Complain>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
