using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.VideoAdService
{
    public class VideoAdService : IVideoAdService
    {
        private readonly IVideoAdRepository _videoAdRepository;
        public VideoAdService(IVideoAdRepository videoAdRepository)
        {
            _videoAdRepository = videoAdRepository;
        }
        public void Create(VideoAd model)
        {
            _videoAdRepository.Create(model);
        }

        public void Delete(VideoAd model)
        {
            _videoAdRepository.Delete(model);
        }

        public List<VideoAd> GetAll()
        {
            return _videoAdRepository.GetAll();
        }

        public VideoAd GetById(int? id)
        {
            return _videoAdRepository.GetById(id);
        }

        public VideoAd HitRead(int? id)
        {
            return _videoAdRepository.HitRead(id);
        }

        public void SetActive(int id)
        {
            _videoAdRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _videoAdRepository.SetDeActive(id);
        }

        public void Update(VideoAd model)
        {
            _videoAdRepository.Update(model);
        }
    }
}
