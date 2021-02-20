using OrderFood.Business.Interfaces;
using OrderFood.Data.Data;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        ApplicationDbContext _db;
        public ProductRepository()
        {
            _db = new ApplicationDbContext();
        }
        public List<Product> GetAllIncluding()
        {
            return _db.Set<Product>().Include("Category").Include("Subcategory").Include("ProductDetail").Include("Country").Include("City").Include("Company").Include("Company.City").Include("Company.Country").Include("City.Companies").Include("City.Products").Include("Comments").Include("Complains").Include("Pictures").Include("Orders").ToList();
        }

        public Product HitRead(int? id)
        {
            var hit = _db.Set<Product>().Where(i => i.Id == id).SingleOrDefault();
            hit.Hit++;
            _db.SaveChanges();
            return hit;
        }

        public Product Like(int? id)
        {
            var like = _db.Set<Product>().FirstOrDefault(i => i.Id == id);
            like.Like++;
            _db.SaveChanges();
            return like;
        }

        public void SetActive(int id)
        {
            var active = _db.Set<Product>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<Product>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
