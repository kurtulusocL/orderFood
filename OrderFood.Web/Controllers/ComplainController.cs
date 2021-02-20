using OrderFood.Service.ComplainService;
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
    public class ComplainController : Controller
    {
        private readonly IComplainService _complainService;
        public ComplainController(IComplainService complainService)
        {
            _complainService = complainService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_complainService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_complainService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Product(int? id, int page = 1)
        {
            return View(_complainService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_complainService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ComplainList(int page = 1)
        {
            return View(_complainService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _complainService.SetActive(id);
            return RedirectToAction(nameof(ComplainList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _complainService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteComplain = _complainService.GetById(id);
            if (deleteComplain != null)
            {
                _complainService.Delete(deleteComplain);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}