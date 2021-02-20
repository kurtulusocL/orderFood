using OrderFood.Entity.Models;
using OrderFood.Service.CategoryService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Audit]
        public ActionResult Index()
        {
            return View(_categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToList());
        }

        [Audit]
        public ActionResult Detail(int? id)
        {
            return View(_categoryService.GetById(id));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 15));
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
        public ActionResult Create(Category model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/category/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _categoryService.Create(model);
                TempData["message"] = "Addition is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the category.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateCategory = _categoryService.GetById(id);
            if (updateCategory != null)
            {
                return View(updateCategory);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/category/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _categoryService.Update(model);
                TempData["message"] = "The update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the category.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CategoryList(int page = 1)
        {
            return View(_categoryService.GetAllIncluding().OrderByDescending(i => i.Products.Count()).ToPagedList(page, 15));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _categoryService.SetActive(id);
            return RedirectToAction(nameof(CategoryList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _categoryService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteCategory = _categoryService.GetById(id);
            if (deleteCategory != null)
            {
                _categoryService.Delete(deleteCategory);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}