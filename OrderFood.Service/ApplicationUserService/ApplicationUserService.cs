using OrderFood.Business.Interfaces;
using OrderFood.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ApplicationUserService
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;
        public ApplicationUserService(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Create(ApplicationUser model)
        {
            _userRepository.Create(model);
        }

        public void Delete(ApplicationUser model)
        {
            _userRepository.Delete(model);
        }

        public List<ApplicationUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public ApplicationUser GetById(string id)
        {
            return _userRepository.GetWithId(id);
        }

        public void SetActive(string id)
        {
            _userRepository.SetActive(id);
        }

        public void SetDeActive(string id)
        {
            _userRepository.SetDeActive(id);
        }

        public void Update(ApplicationUser model)
        {
            _userRepository.Update(model);
        }
    }
}
