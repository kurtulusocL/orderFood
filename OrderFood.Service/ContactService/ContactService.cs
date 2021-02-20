using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public void Create(Contact model)
        {
            _contactRepository.Create(model);
        }

        public void Delete(Contact model)
        {
            _contactRepository.Delete(model);
        }

        public List<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

        public Contact GetById(int? id)
        {
            return _contactRepository.GetById(id);
        }

        public Contact HitRead(int? id)
        {
            return _contactRepository.HitRead(id);
        }

        public void SetActive(int id)
        {
            _contactRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _contactRepository.SetDeActive(id);
        }

        public void Update(Contact model)
        {
            _contactRepository.Update(model);
        }
    }
}
