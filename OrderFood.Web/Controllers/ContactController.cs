using OrderFood.Entity.Models;
using OrderFood.Service.ContactService;
using OrderFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Audit]
        public ActionResult Index()
        {
            return View(_contactService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _HitCount(int? id)
        {
            return PartialView(_contactService.HitRead(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL()
        {
            return View(_contactService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_contactService.GetById(id));
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
        public ActionResult Create(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contactService.Create(model);
                    TempData["message"] = "Addition is successful.";
                }                
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding contact information.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updateContact = _contactService.GetById(id);
            if (updateContact != null)
            {
                return View(updateContact);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contactService.Update(model);
                    TempData["message"] = "The update process is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the contact information.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ContactList()
        {
            return View(_contactService.GetAll().OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _contactService.SetActive(id);
            return RedirectToAction(nameof(ContactList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _contactService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteContact = _contactService.GetById(id);
            if (deleteContact != null)
            {
                _contactService.Delete(deleteContact);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}