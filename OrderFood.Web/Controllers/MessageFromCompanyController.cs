using OrderFood.Service.MessageFromCompanyService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class MessageFromCompanyController : Controller
    {
        private readonly IMessageFromCompanyService _companyMessageService;
        public MessageFromCompanyController(IMessageFromCompanyService companyMessageService)
        {
            _companyMessageService = companyMessageService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_companyMessageService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_companyMessageService.GetById(id));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _companyMessageService.SetActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _companyMessageService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteMessage = _companyMessageService.GetById(id);
            if (deleteMessage != null)
            {
                _companyMessageService.Delete(deleteMessage);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}