using OrderFood.Entity.Models;
using OrderFood.Service.ToMakeService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class ToMakeController : Controller
    {
        private readonly IToMakeService _toMakeService;
        public ToMakeController(IToMakeService toMakeService)
        {
            _toMakeService = toMakeService;
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Index(int page = 1)
        {
            return View(_toMakeService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_toMakeService.GetById(id));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_toMakeService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.IsFinished == true).ToPagedList(page, 30));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToMake model)
        {
            try
            {
                model.UserId = Convert.ToString(Session["adminId"]);
                if (ModelState.IsValid)
                {
                    _toMakeService.Create(model);
                    TempData["message"] = "The task has been successfully added.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the task.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            var updateTask = _toMakeService.GetById(id);
            if (updateTask != null)
            {
                return View(updateTask);
            }
            return RedirectToAction(nameof(Edit));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ToMake model)
        {
            try
            {
                model.UserId = Convert.ToString(Session["adminId"]);
                if (ModelState.IsValid)
                {
                    _toMakeService.Update(model);
                    TempData["message"] = "The task has been updated successfully.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the task.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult TaskList(int page = 1)
        {
            return View(_toMakeService.GetAll().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult Active(int id)
        {
            _toMakeService.SetActive(id);
            return RedirectToAction(nameof(TaskList));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult DeActive(int id)
        {
            _toMakeService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult KeepTask(int id)
        {
            _toMakeService.Continue(id);
            return RedirectToAction(nameof(Index));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Done(int id)
        {
            _toMakeService.Finished(id);
            return RedirectToAction(nameof(Index));
        }

        [Audit]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var deleteTask = _toMakeService.GetById(id);
            if (deleteTask != null)
            {
                _toMakeService.Delete(deleteTask);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}