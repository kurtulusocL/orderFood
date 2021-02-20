using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.VideoAdService
{
    public interface IVideoAdService
    {
        void Create(VideoAd model);
        void Update(VideoAd model);
        List<VideoAd> GetAll();
        VideoAd GetById(int? id);       
        void Delete(VideoAd model);
        VideoAd HitRead(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
