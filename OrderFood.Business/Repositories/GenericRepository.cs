using OrderFood.Business.Interfaces;
using OrderFood.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        ApplicationDbContext _db;
        public GenericRepository()
        {
            _db = new ApplicationDbContext();
        }
        public void Create(T model)
        {
            _db.Set<T>().Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(T model)
        {
            _db.Set<T>().Remove(model);
            _db.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(int? id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Update(T model)
        {
            _db.Set<T>().Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
