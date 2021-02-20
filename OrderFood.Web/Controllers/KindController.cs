using OrderFood.Entity.Models;
using OrderFood.Service.KindService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class KindController : Controller
    {
        private readonly IKindService _kindService;
        public KindController(IKindService kindService)
        {
            _kindService = kindService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_kindService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Companies.Count()).ToPagedList(page, 20));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Create()
        {
            return View();
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kind model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _kindService.Create(model);
                    TempData["message"] = "Addition is successful.";
                }                
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the company type.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateKind = _kindService.GetById(id);
            if (updateKind != null)
            {
                return View(updateKind);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kind model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _kindService.Update(model);
                    TempData["message"] = "The update process is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the company type.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult KindList(int page = 1)
        {
            return View(_kindService.GetAllIncluding().OrderByDescending(i => i.Companies.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _kindService.SetActive(id);
            return RedirectToAction(nameof(KindList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _kindService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteKind = _kindService.GetById(id);
            if (deleteKind != null)
            {
                _kindService.Delete(deleteKind);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}