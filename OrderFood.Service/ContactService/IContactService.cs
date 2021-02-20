using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ContactService
{
    public interface IContactService
    {
        void Create(Contact model);
        void Update(Contact model);
        List<Contact> GetAll();
        Contact GetById(int? id);
        void Delete(Contact model);
        Contact HitRead(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
