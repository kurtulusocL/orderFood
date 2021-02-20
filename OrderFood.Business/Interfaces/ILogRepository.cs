using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Business.Interfaces
{
    public interface ILogRepository:IGenericRepository<Audit>
    {
        Audit GetWithId(Guid id);
        void DeleteAudit(Audit model);
    }
}
