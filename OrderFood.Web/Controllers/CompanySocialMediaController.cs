using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class CompanySocialMediaController : Controller
    {
        private readonly ICompanySocialMediaService _socialMediaService;
        public CompanySocialMediaController(ICompanySocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_socialMediaService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_socialMediaService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SocialMediaList(int page = 1)
        {
            return View(_socialMediaService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
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