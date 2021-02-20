using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ProfilePhotoService
{
    public class ProfilePhotoService : IProfilePhotoService
    {
        private readonly IProfilePhotoRepository _photoRepository;
        public ProfilePhotoService(IProfilePhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }
        public void Create(ProfilePhoto model)
        {
            _photoRepository.Create(model);
        }

        public void Delete(ProfilePhoto model)
        {
            _photoRepository.Delete(model);
        }

        public List<ProfilePhoto> GetAll()
        {
            return _photoRepository.GetAll();
        }

        public ProfilePhoto GetById(int? id)
        {
            return _photoRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _photoRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _photoRepository.SetDeActive(id);
        }

        public void Update(ProfilePhoto model)
        {
            _photoRepository.Update(model);
        }
    }
}
