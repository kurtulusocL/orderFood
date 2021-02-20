using OrderFood.Entity.Models;
using OrderFood.Service.CategoryService;
using OrderFood.Service.SubcategoryService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICategoryService _categoryService;
        public SubcategoryController(ISubcategoryService subcategoryService, ICategoryService categoryService)
        {
            _subcategoryService = subcategoryService;
            _categoryService = categoryService;
        }

        [Audit]
        public ActionResult Detail(int? id)
        {
            return View(_subcategoryService.GetById(id));
        }

        [Audit]
        public ActionResult Category(int? id, int page = 1)
        {
            return View(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CategoryId == id && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 21));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CategoryList(int? id, int page = 1)
        {
            return View(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CategoryId == id).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 20));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Subcategories.Count()).ToList();
            return View();
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subcategory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _subcategoryService.Create(model);
                    TempData["message"] = "Addition is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding a food type.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateSubcate = _subcategoryService.GetById(id);
            if (updateSubcate != null)
            {
                return View(updateSubcate);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subcategory model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _subcategoryService.Update(model);
                    TempData["message"] = "The update process is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the food type.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SubcategoryList(int page = 1)
        {
            return View(_subcategoryService.GetAllIncluding().OrderByDescending(i => i.Products.Count()).ToPagedList(page, 20));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _subcategoryService.SetActive(id);
            return RedirectToAction(nameof(SubcategoryList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _subcategoryService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteSubcate = _subcategoryService.GetById(id);
            if (deleteSubcate != null)
            {
                _subcategoryService.Delete(deleteSubcate);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}