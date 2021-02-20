using OrderFood.Service.LogService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    [Audit]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_logService.GetAll().OrderByDescending(i => i.Timestamp).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult UserLog(int page = 1)
        {
            return View(_logService.GetAll().Where(i => i.UserName != null && i.UserName != "Anonymous").OrderByDescending(i => i.Timestamp).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult PhoneUser(int page = 1)
        {
            return View(_logService.GetAll().Where(i => i.IsMobile == true && i.DeviceModel != null && i.DeviceModel != "Unknown").OrderByDescending(i => i.Timestamp).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ComputerUser(int page = 1)
        {
            return View(_logService.GetAll().Where(i => i.IsMobile == false && i.DeviceModel == null).OrderByDescending(i => i.Timestamp).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SearchLog(int page = 1)
        {
            return View(_logService.GetAll().OrderByDescending(i => i.Timestamp).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(Guid id)
        {
            return View(_logService.GetWithId(id));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(Guid id)
        {
            var deleteLog = _logService.GetWithId(id);
            if (deleteLog != null)
            {
                _logService.DeleteAudit(deleteLog);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}