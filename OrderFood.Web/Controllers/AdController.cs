using OrderFood.Entity.Models;
using OrderFood.Service.AdService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdService _adService;
        public AdController(IAdService adService)
        {
            _adService = adService;
        }
        public ActionResult _HitCount(int? id)
        {
            return PartialView(_adService.HitRead(id));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_adService.GetAll().Where(i => i.IsConfirmed == true).OrderBy(i => i.DeletedTime).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_adService.GetById(id));
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
        public ActionResult Create(Ad model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/reklam/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _adService.Create(model);
                TempData["message"] = "Addition is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the ad.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateAd = _adService.GetById(id);
            if (updateAd!=null)
            {
                return View(updateAd);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ad model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/reklam/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _adService.Update(model);
                TempData["message"] = "The update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the ad.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AdList(int page = 1)
        {
            return View(_adService.GetAll().OrderBy(i => i.DeletedTime).ToPagedList(page, 20));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _adService.SetActive(id);
            return RedirectToAction(nameof(AdList));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _adService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteAd = _adService.GetById(id);
            if (deleteAd!=null)
            {
                _adService.Delete(deleteAd);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}