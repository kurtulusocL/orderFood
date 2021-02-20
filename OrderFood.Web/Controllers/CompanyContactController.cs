using OrderFood.Service.CompanyContactService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class CompanyContactController : Controller
    {
        private readonly ICompanyContactService _companyContactService;
        public CompanyContactController(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_companyContactService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int? id)
        {
            return View(_companyContactService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_companyContactService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 15));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ContactList(int page = 1)
        {
            return View(_companyContactService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _companyContactService.SetActive(id);
            return RedirectToAction(nameof(ContactList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _companyContactService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteContact = _companyContactService.GetById(id);
            if (deleteContact != null)
            {
                _companyContactService.Delete(deleteContact);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}