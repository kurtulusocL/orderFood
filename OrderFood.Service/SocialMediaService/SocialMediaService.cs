using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SocialMediaService
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ISocialMediaRepository _socialMediaRepository;
        public SocialMediaService(ISocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }
        public void Create(SocialMedia model)
        {
            _socialMediaRepository.Create(model);
        }

        public void Delete(SocialMedia model)
        {
            _socialMediaRepository.Delete(model);
        }

        public List<SocialMedia> GetAll()
        {
            return _socialMediaRepository.GetAll();
        }

        public SocialMedia GetById(int? id)
        {
            return _socialMediaRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _socialMediaRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _socialMediaRepository.SetDeActive(id);
        }

        public void Update(SocialMedia model)
        {
            _socialMediaRepository.Update(model);
        }
    }
}
