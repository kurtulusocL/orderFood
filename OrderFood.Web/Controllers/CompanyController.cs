using Newtonsoft.Json;
using OrderFood.Entity.Models;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Service.ComplainService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static OrderFood.Web.Controllers.HomeController.CaptchaResult;

namespace OrderFood.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyContactService _companyContactService;
        private readonly ICompanySocialMediaService _companySocialMediaService;
        private readonly ICommentService _commentService;
        private readonly IComplainService _complainService;
        public CompanyController(ICompanyService companyService, ICompanySocialMediaService companySocialMediaService, ICompanyContactService companyContactService, ICommentService commentService, IComplainService complainService)
        {
            _companyService = companyService;
            _companyContactService = companyContactService;
            _companySocialMediaService = companySocialMediaService;
            _commentService = commentService;
            _complainService = complainService;
        }

        [Audit]
        public ActionResult Index(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 21));
        }

        [Audit]
        public ActionResult Detail(int? id)
        {
            return View(_companyService.GetById(id));
        }

        [Audit]
        public ActionResult _HitRead(int? id)
        {
            return PartialView(_companyService.HitRead(id));
        }

        [Audit]
        public ActionResult Like(int id, Company model)
        {
            _companyService.Like(id); 
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Detail", controller = "Company", id = model.Id }));
        }

        [Audit]
        public ActionResult Country(int? id, int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }

        [Audit]
        public ActionResult City(int? id, int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }

        [Audit]
        public ActionResult Kind(int? id, int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.KindId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }

        [Audit]
        public ActionResult _CreateComment(int? id)
        {
            Session["companyId"] = id;
            return PartialView();
        }

        [Audit]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateComment(Comment model)
        {
            try
            {
                var response = Request["g-recaptcha-response"];
                const string secret = "6LelQDMaAAAAAEE-zkCOebE3l8SiI3KiUi1m4ASs";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                if (!captchaResponse.Success)
                    TempData["Message"] = "Verifique la seguridad.";
                else
                    TempData["Message"] = "La seguridad se ha verificado correctamente.";

                if (User.IsInRole("Tienda") && User.Identity.IsAuthenticated == true)
                {
                    model.UserId = Convert.ToString(Session["TiendaId"]);
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    _commentService.Create(model);
                    TempData["message"] = "Su comentario ha sido agregado con éxito.";
                }
                if (User.IsInRole("Users") && User.Identity.IsAuthenticated == true)
                {
                    model.UserId = Convert.ToString(Session["userId"]);
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    _commentService.Create(model);
                    TempData["message"] = "Su comentario ha sido agregado con éxito.";
                }
                if (User.Identity.IsAuthenticated == false)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    _commentService.Create(model);
                    TempData["message"] = "Su comentario ha sido agregado con éxito.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Recibimos un error al guardar tu comentario. Verifique los campos relevantes y comente nuevamente.";
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Detail", controller = "Company", id = model.CompanyId }));
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Detail", controller = "Company", id = model.CompanyId }));
        }

        [Audit]
        public ActionResult Complain(int? id, int? companyId)
        {
            Session["companyId"] = id;
            return View();
        }

        [Audit]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complain(Complain model)
        {
            try
            {
                var response = Request["g-recaptcha-response"];
                const string secret = "6LelQDMaAAAAAEE-zkCOebE3l8SiI3KiUi1m4ASs";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                if (!captchaResponse.Success)
                    TempData["Message"] = "Verifique la seguridad.";
                else
                    TempData["Message"] = "La seguridad se ha verificado correctamente.";

                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                if (User.IsInRole("Tienda") && User.Identity.IsAuthenticated == true)
                {
                    model.UserId = Convert.ToString(Session["TiendaId"]);
                }
                if (User.IsInRole("Users") && User.Identity.IsAuthenticated == true)
                {
                    model.UserId = Convert.ToString(Session["userId"]);
                }
                if (ModelState.IsValid)
                {
                    _complainService.Create(model);
                    TempData["message"] = "Tu queja se ha agregado correctamente.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al guardar su informe. Compruebe los campos correspondientes y vuelva a intentarlo.";
                return RedirectToAction(nameof(Complain));
            }
            return RedirectToAction(nameof(Complain));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyDetail(int? id)
        {
            return View(_companyService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CountryList(int? id, int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CityList(int? id, int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult KindList(int? id, int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.KindId == id).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyList(int page = 1)
        {
            return View(_companyService.GetAllIncluding().OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _companyService.SetActive(id);
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _companyService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteCompany = _companyService.GetById(id);
            if (deleteCompany != null)
            {
                _companyService.Delete(deleteCompany);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}