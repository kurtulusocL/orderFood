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
    public class SendMailRepository : GenericRepository<SendMail>, ISendMailRepository
    {
        ApplicationDbContext _db;
        public SendMailRepository()
        {
            _db = new ApplicationDbContext();
        }
        public void SetActive(int id)
        {
            var active = _db.Set<SendMail>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var deActive = _db.Set<SendMail>().Where(i => i.Id == id).FirstOrDefault();
            deActive.IsConfirmed = false;
            _db.SaveChanges();
        }
    }
}
