using OrderFood.Service.CancelRequestService;
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
    public class CancelRequestController : Controller
    {
        private readonly ICancelRequestService _cancelRequestService;
        public CancelRequestController(ICancelRequestService cancelRequestService)
        {
            _cancelRequestService = cancelRequestService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_cancelRequestService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_cancelRequestService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_cancelRequestService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CancelList(int page = 1)
        {
            return View(_cancelRequestService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _cancelRequestService.SetActive(id);
            return RedirectToAction(nameof(CancelList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _cancelRequestService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteRequest = _cancelRequestService.GetById(id);
            if (deleteRequest != null)
            {
                _cancelRequestService.Delete(deleteRequest);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}