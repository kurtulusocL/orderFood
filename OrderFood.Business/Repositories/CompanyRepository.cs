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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        ApplicationDbContext _db;
        public CompanyRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Company> GetAllIncluding()
        {
            return _db.Set<Company>().Include("Country").Include("City").Include("Kind").Include("Complains").Include("Comments").Include("Pictures").Include("CompanyContacts").Include("CompanySocialMedias").Include("Products").Include("CancelRequests").Include("Orders").ToList();
        }

        public Company HitRead(int? id)
        {
            var hit = _db.Set<Company>().Where(i => i.Id == id).SingleOrDefault();
            hit.Hit++;
            _db.SaveChanges();
            return hit;
        }

        public void Like(int id)
        {
            var like = _db.Set<Company>().Where(i => i.Id == id).FirstOrDefault();
            like.Like++;
            _db.SaveChanges();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Company>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Company>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
