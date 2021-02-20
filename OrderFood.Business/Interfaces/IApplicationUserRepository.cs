using OrderFood.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetWithId(string id);
        void SetActive(string id);
        void SetDeActive(string id);
    }
}