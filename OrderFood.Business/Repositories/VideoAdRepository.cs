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
    public class VideoAdRepository : GenericRepository<VideoAd>, IVideoAdRepository
    {
        ApplicationDbContext _db;
        public VideoAdRepository()
        {
            _db = new ApplicationDbContext();
        }
        public VideoAd HitRead(int? id)
        {
            var hit = _db.Set<VideoAd>().Where(i => i.Id == id).SingleOrDefault();
            hit.Hit++;
            _db.SaveChanges();
            return hit;
        }

        public void SetActive(int id)
        {
            var active = _db.Set<VideoAd>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }

        public void SetDeActive(int id)
        {
            var active = _db.Set<VideoAd>().Where(i => i.Id == id).FirstOrDefault();
            active.IsConfirmed = true;
            _db.SaveChanges();
        }
    }
}
