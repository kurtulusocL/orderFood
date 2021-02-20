using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ProfilePhotoService
{
    public interface IProfilePhotoService
    {
        void Create(ProfilePhoto model);
        void Update(ProfilePhoto model);
        List<ProfilePhoto> GetAll();
        ProfilePhoto GetById(int? id);
        void Delete(ProfilePhoto model);      
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
