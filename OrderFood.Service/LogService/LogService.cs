using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public void DeleteAudit(Audit model)
        {
            _logRepository.DeleteAudit(model);
        }

        public List<Audit> GetAll()
        {
            return _logRepository.GetAll();
        }

        public Audit GetWithId(Guid id)
        {
            return _logRepository.GetWithId(id);
        }
    }
}
