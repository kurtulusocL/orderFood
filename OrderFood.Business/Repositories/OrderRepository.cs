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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        ApplicationDbContext _db;
        public OrderRepository()
        {
            _db = new ApplicationDbContext();
        }

        public void ActiveSent(int id)
        {
            var active = _db.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
            active.IsSent = true;
            _db.SaveChanges();
        }

        public void DeActiveSent(int id)
        {
            var active = _db.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
            active.IsSent = false;
            _db.SaveChanges();
        }

        public List<Order> GetAllIncluding()
        {
            return _db.Set<Order>().Include("Company").Include("Product").Include("Product.City").Include("Product.Country").Include("Product.Company").Include("Company.City").Include("Company.Country").Include("Payment").Include("Product.Orders").Include("Company.Orders").ToList();
        }

        public Order HitRead(int? id)
        {
            var hit = _db.Set<Order>().Where(i => i.Id == id).SingleOrDefault();
            hit.Hit++;
            _db.SaveChanges();
            return hit;
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
