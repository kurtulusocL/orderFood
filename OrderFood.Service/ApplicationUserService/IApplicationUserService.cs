using OrderFood.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ApplicationUserService
{
    public interface IApplicationUserService
    {
        void Create(ApplicationUser model);
        void Update(ApplicationUser model);
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        void Delete(ApplicationUser model);       
        void SetActive(string id);
        void SetDeActive(string id);
    }
}
