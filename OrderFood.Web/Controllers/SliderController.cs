using OrderFood.Entity.Models;
using OrderFood.Service.SliderService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_sliderService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
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
        public ActionResult Create(Slider model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/slider/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _sliderService.Create(model);
                TempData["message"] = "Addition is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the slider image.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateSlider = _sliderService.GetById(id);
            if (updateSlider != null)
            {
                return View(updateSlider);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/slider/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _sliderService.Update(model);
                TempData["message"] = "The update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the slider image.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SliderList(int page = 1)
        {
            return View(_sliderService.GetAll().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 35));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _sliderService.SetActive(id);
            return RedirectToAction(nameof(SliderList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _sliderService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteSlider = _sliderService.GetById(id);
            if (deleteSlider != null)
            {
                _sliderService.Delete(deleteSlider);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}