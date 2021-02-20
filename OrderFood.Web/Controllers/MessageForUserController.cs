using OrderFood.Entity.Models;
using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.MessageForUserService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class MessageForUserController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly IMessageForUserService _messageService;
        public MessageForUserController(IApplicationUserService userService, IMessageForUserService messageService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult UserList(int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.RespondTitle != "Admin" && i.RespondTitle != "Asistan Admin" && i.RespondTitle != "Yardımcı Admin").OrderByDescending(i => i.CreatedDate).ToPagedList(page, 35));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_messageService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant,Users,Tienda")]
        public ActionResult _HitCount(int? id)
        {
            return PartialView(_messageService.HitRead(id));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_messageService.GetById(id));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Create(string id)
        {
            var company = _userService.GetById(id);
            var user = _userService.GetById(id);
            Session["userId"] = user.Id;
            Session["TiendaId"] = company.Id;
            return View();
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageForUser model)
        {
            try
            {
                model.UserId = Convert.ToString(Session["userId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);
                _messageService.Create(model);
                TempData["message"] = "The message has been sent successfully.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error was encountered while sending the message.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(string userid, int? id)
        {
            var company = _userService.GetById(userid);
            var user = _userService.GetById(userid);
            Session["userId"] = user.Id;
            Session["TiendaId"] = company.Id;

            var updateMessage = _messageService.GetById(id);
            if (updateMessage != null)
            {
                return View(updateMessage);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MessageForUser model)
        {
            try
            {
                model.UserId = Convert.ToString(Session["userId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);
                _messageService.Update(model);
                TempData["message"] = "The message has been successfully updated and sent.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error was encountered while updating and sending the message.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult MessageList(int page = 1)
        {
            return View(_messageService.GetAll().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _messageService.SetActive(id);
            return RedirectToAction(nameof(MessageList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _messageService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteMessage = _messageService.GetById(id);
            if (deleteMessage != null)
            {
                _messageService.Delete(deleteMessage);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}