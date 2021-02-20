using OrderFood.Entity.Models;
using OrderFood.Service.SendMailService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class SendMailController : Controller
    {
        private readonly ISendMailService _sendMailService;
        public SendMailController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_sendMailService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_sendMailService.GetById(id));
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
        public ActionResult Create(SendMail model)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(model.Sender, "Kurtuluş Öcal");
                mail.Priority = MailPriority.High;
                mail.Subject = model.Subject;
                mail.To.Add(new MailAddress(model.Reciever, ""));
                mail.Body = model.Text;
                mail.IsBodyHtml = true;

                NetworkCredential enter = new NetworkCredential("emailaddress","password");
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = enter;
                client.Send(mail);

                _sendMailService.Create(model);
                TempData["message"] = "E-Mail sending is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while sending the E-Mail.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SentMailList(int page = 1)
        {
            return View(_sendMailService.GetAll().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 35));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult SetActive(int id)
        {
            _sendMailService.SetActive(id);
            return RedirectToAction(nameof(SentMailList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _sendMailService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteMail = _sendMailService.GetById(id);
            if (deleteMail != null)
            {
                _sendMailService.Delete(deleteMail);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}