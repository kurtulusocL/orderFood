using OrderFood.Service.ProfilePhotoService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class ProfilePhotoController : Controller
    {
        private readonly IProfilePhotoService _photoService;
        public ProfilePhotoController(ProfilePhotoService photoService)
        {
            _photoService = photoService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_photoService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult PhotoList(int page = 1)
        {
            return View(_photoService.GetAll().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _photoService.SetActive(id);
            return RedirectToAction(nameof(PhotoList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _photoService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deletePhoto = _photoService.GetById(id);
            if (deletePhoto != null)
            {
                _photoService.Delete(deletePhoto);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}