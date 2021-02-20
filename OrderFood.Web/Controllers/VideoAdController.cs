using OrderFood.Entity.Models;
using OrderFood.Service.VideoAdService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class VideoAdController : Controller
    {
        private readonly IVideoAdService _videoAdService;
        public VideoAdController(IVideoAdService videoAdService)
        {
            _videoAdService = videoAdService;
        }
        public ActionResult _HitCount(int? id)
        {
            return View(_videoAdService.HitRead(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_videoAdService.GetAll().Where(i => i.IsConfirmed == true).OrderBy(i => i.DeletedTime).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_videoAdService.GetById(id));
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
        public ActionResult Create(VideoAd model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _videoAdService.Create(model);
                    TempData["message"] = "Addition is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding a video ad.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateVideoAd = _videoAdService.GetById(id);
            if (updateVideoAd != null)
            {
                return View(updateVideoAd);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VideoAd model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _videoAdService.Update(model);
                    TempData["message"] = "The update process is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the video ad.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult VideoAdList(int page = 1)
        {
            return View(_videoAdService.GetAll().OrderBy(i => i.CreatedDate).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _videoAdService.SetActive(id);
            return RedirectToAction(nameof(VideoAdList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _videoAdService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteVideoAd = _videoAdService.GetById(id);
            if (deleteVideoAd != null)
            {
                _videoAdService.Delete(deleteVideoAd);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}