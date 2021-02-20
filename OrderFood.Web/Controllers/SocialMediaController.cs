using OrderFood.Entity.Models;
using OrderFood.Service.SocialMediaService;
using OrderFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;
        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL()
        {
            return View(_socialMediaService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList());
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
        public ActionResult Create(SocialMedia model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/social/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _socialMediaService.Create(model);
                TempData["message"] = "Addition is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding a social media account.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateSocialMedia = _socialMediaService.GetById(id);
            if (updateSocialMedia != null)
            {
                return View(updateSocialMedia);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SocialMedia model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/social/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _socialMediaService.Update(model);
                TempData["message"] = "The update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the social media account.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SocialMediaList()
        {
            return View(_socialMediaService.GetAll().OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _socialMediaService.SetActive(id);
            return RedirectToAction(nameof(SocialMediaList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _socialMediaService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteSocialMedia = _socialMediaService.GetById(id);
            if (deleteSocialMedia != null)
            {
                _socialMediaService.Delete(deleteSocialMedia);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}