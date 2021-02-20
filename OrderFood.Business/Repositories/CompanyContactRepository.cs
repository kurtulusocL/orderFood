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
    public class CompanyContactRepository : GenericRepository<CompanyContact>, ICompanyContactRepository
    {
        ApplicationDbContext _db;
        public CompanyContactRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<CompanyContact> GetAllIncluding()
        {
            return _db.Set<CompanyContact>().Include("Company").Include("Company.Country").Include("Company.City").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<CompanyContact>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<CompanyContact>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
