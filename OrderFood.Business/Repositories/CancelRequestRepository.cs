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
    public class CancelRequestRepository : GenericRepository<CancelRequest>, ICancelRequestRepository
    {
        ApplicationDbContext _db;
        public CancelRequestRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<CancelRequest> GetAllIncluding()
        {
            return _db.Set<CancelRequest>().Include("Company").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<CancelRequest>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeAcive(int id)
        {
            var deActive = _db.Set<CancelRequest>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
