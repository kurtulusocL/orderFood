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
    public class LogRepository : GenericRepository<Audit>, ILogRepository
    {
        ApplicationDbContext _db;
        public LogRepository()
        {
            _db = new ApplicationDbContext();
        }
        public void DeleteAudit(Audit model)
        {
            _db.Set<Audit>().Remove(model);
            _db.SaveChanges();
        }

        public Audit GetWithId(Guid id)
        {
            return _db.Set<Audit>().Find(id);
        }
    }
}
