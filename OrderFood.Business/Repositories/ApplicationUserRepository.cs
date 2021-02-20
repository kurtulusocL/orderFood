using OrderFood.Business.Interfaces;
using OrderFood.Data.Data;
using OrderFood.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        ApplicationDbContext _db;
        public ApplicationUserRepository()
        {
            _db = new ApplicationDbContext();
        }

        public ApplicationUser GetWithId(string id)
        {
            return _db.Set<ApplicationUser>().Find(id);
        }

        public void SetActive(string id)
        {
            var active = _db.Set<ApplicationUser>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(string id)
        {
            var deActive = _db.Set<ApplicationUser>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
