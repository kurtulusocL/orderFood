using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.LogService
{
    public interface ILogService
    {
        List<Audit> GetAll();
        Audit GetWithId(Guid id);
        void DeleteAudit(Audit model);
    }
}
