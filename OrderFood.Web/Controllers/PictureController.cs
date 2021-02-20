using OrderFood.Service.PictureService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class PictureController : Controller
    {
        private readonly IPictureService _pictureService;
        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Product(int? id, int page = 1)
        {
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult PictureList(int page = 1)
        {
            return View(_pictureService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(PictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deletePicture = _pictureService.GetById(id);
            if (deletePicture != null)
            {
                _pictureService.Delete(deletePicture);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}