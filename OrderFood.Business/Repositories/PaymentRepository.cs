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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        ApplicationDbContext _db;
        public PaymentRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Payment> GetAllIncluding()
        {
            return _db.Set<Payment>().Include("Orders").ToList();
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Payment>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Payment>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
