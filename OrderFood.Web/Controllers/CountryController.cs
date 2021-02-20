using OrderFood.Entity.Models;
using OrderFood.Service.CountryService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [Audit]
        public ActionResult Index()
        {
            return View(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0 || i.Companies.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }

        [Audit]
        public ActionResult Detail(int? id)
        {
            return View(_countryService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 20));
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
        public ActionResult Create(Country model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/country/" + image.FileName));
                    model.Photo = image.FileName;
                }
                TempData["message"] = "Addition is successful.";
                _countryService.Create(model);
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the country.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateCountry = _countryService.GetById(id);
            if (updateCountry != null)
            {
                return View(updateCountry);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/country/" + image.FileName));
                    model.Photo = image.FileName;
                }
                TempData["message"] = "The update process is successful.";
                _countryService.Update(model);
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the country.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CountryList(int page = 1)
        {
            return View(_countryService.GetAllIncluding().OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _countryService.SetActive(id);
            return RedirectToAction(nameof(CountryList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _countryService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteCountry = _countryService.GetById(id);
            if (deleteCountry != null)
            {
                _countryService.Delete(deleteCountry);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}