using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.PictureService
{
    public interface IPictureService
    {
        void Create(Picture model);
        void Update(Picture model);
        List<Picture> GetAllIncluding();
        Picture GetById(int? id);
        void Delete(Picture model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
