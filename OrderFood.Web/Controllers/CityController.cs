using OrderFood.Entity.Models;
using OrderFood.Service.CityService;
using OrderFood.Service.CountryService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OrderFood.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        public CityController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        [Audit]
        public ActionResult Index(int page = 1)
        {
            return View(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Audit]
        public ActionResult Detail(int? id)
        {
            return View(_cityService.GetById(id));
        }

        [Audit]
        public ActionResult Country(int? id, int page = 1)
        {
            return View(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 20));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CountryList(int? id, int page = 1)
        {
            return View(_cityService.GetAllIncluding().Where(i => i.CountryId == id).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Create()
        {
            ViewBag.Countries = _countryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Cities.Count()).ToList();
            return View();
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cityService.Create(model);
                    TempData["message"] = "Addition is successful.";
                }                
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the city.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            ViewBag.Countries = _countryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Cities.Count()).ToList();
            var updateCity = _cityService.GetById(id);
            if (updateCity != null)
            {
                return View(updateCity);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cityService.Update(model);
                    TempData["message"] = "The update process is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the city.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CityList(int page = 1)
        {
            return View(_cityService.GetAllIncluding().OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _cityService.SetActive(id);
            return RedirectToAction(nameof(CityList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _cityService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteCity = _cityService.GetById(id);
            if (deleteCity != null)
            {
                _cityService.Delete(deleteCity);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}