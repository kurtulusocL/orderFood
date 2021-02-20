using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.SocialMediaService
{
    public interface ISocialMediaService
    {
        void Create(SocialMedia model);
        void Update(SocialMedia model);
        List<SocialMedia> GetAll();
        SocialMedia GetById(int? id);
        void Delete(SocialMedia model);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
