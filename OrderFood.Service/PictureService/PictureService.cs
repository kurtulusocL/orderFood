using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.PictureService
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }
        public void Create(Picture model)
        {
            _pictureRepository.Create(model);
        }

        public void Delete(Picture model)
        {
            _pictureRepository.Delete(model);
        }

        public List<Picture> GetAllIncluding()
        {
            return _pictureRepository.GetAllIncluding();
        }

        public Picture GetById(int? id)
        {
            return _pictureRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _pictureRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _pictureRepository.SetDeActive(id);
        }

        public void Update(Picture model)
        {
            _pictureRepository.Update(model);
        }
    }
}
