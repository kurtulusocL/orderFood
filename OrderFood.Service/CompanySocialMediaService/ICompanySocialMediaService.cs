using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CompanySocialMediaService
{
    public interface ICompanySocialMediaService
    {
        void Create(CompanySocialMedia model);
        void Update(CompanySocialMedia model);
        List<CompanySocialMedia> GetAllIncluding();
        CompanySocialMedia GetById(int? id);
        void Delete(CompanySocialMedia model);        
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
