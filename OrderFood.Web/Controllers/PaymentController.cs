using OrderFood.Entity.Models;
using OrderFood.Service.PaymentService;
using OrderFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL()
        {
            return View(_paymentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToList());
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
        public ActionResult Create(Payment model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/payment/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _paymentService.Create(model);
                TempData["message"] = "Addition is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the payment system.";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Edit(int id)
        {
            var updatePay = _paymentService.GetById(id);
            if (updatePay != null)
            {
                return View(updatePay);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payment model, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/payment/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _paymentService.Update(model);
                TempData["message"] = "Update is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the payment system.";
                return RedirectToAction(nameof(Edit));
            }
            return RedirectToAction(nameof(Edit));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult PaymentList()
        {
            return View(_paymentService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _paymentService.SetActive(id);
            return RedirectToAction(nameof(PaymentList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _paymentService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deletePay = _paymentService.GetById(id);
            if (deletePay != null)
            {
                _paymentService.Delete(deletePay);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}