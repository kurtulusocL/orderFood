using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.CancelRequestService;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.ComplainService;
using OrderFood.Service.LogService;
using OrderFood.Service.OrderService;
using OrderFood.Service.ProductService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    [Audit]
    public class AdminController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly ICompanyService _companyService;
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        private readonly IComplainService _complainService;
        private readonly IOrderService _orderService;
        private readonly ICancelRequestService _cancelService;
        private readonly ILogService _logService;
        public AdminController(IApplicationUserService userService, ICompanyService companyService, IProductService productService, ICommentService commentService, IComplainService complainService, IOrderService orderService, ICancelRequestService cancelService, ILogService logService)
        {
            _userService = userService;
            _companyService = companyService;
            _productService = productService;
            _commentService = commentService;
            _complainService = complainService;
            _orderService = orderService;
            _cancelService = cancelService;
            _logService = logService;
        }
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Index(string userId)
        {
            userId = Convert.ToString(Session["adminId"]);
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("Login", "Account");
            if (User.Identity.IsAuthenticated == true)
            {
                var userHome = _userService.GetAll().Where(i => i.Id == userId && i.IsConfirmed == true).ToList();
                return View(userHome);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AllUser()
        {
            ViewBag.AktifKullanici = HttpContext.Application["AktifKullanici"];
            ViewBag.ToplamZiyaretci = HttpContext.Application["ToplamZiyaretci"];
            return View();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AdminList()
        {
            return View(_userService.GetAll().Where(i => i.IsConfirmed == true && i.RespondTitle == "Admin" || i.RespondTitle == "Yardımcı Admin" || i.RespondTitle == "Asistan Admin").OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AllAdminList()
        {
            return View(_userService.GetAll().Where(i => i.RespondTitle == "Admin" || i.RespondTitle == "Yardımcı Admin" || i.RespondTitle == "Asistan Admin").OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AdminDetail(string id)
        {
            return View(_userService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult UserList(int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.IsConfirmed == true && i.RespondTitle == null).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AllUserList(int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.RespondTitle == null).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult UserDetail(string id)
        {
            return View(_userService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyList(int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.IsConfirmed == true && i.RespondTitle != "Admin" && i.RespondTitle != null && i.RespondTitle != "Yardımcı Admin" && i.RespondTitle != "Asistan Admin").OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult AllCompanyList(int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.RespondTitle != "Admin" && i.RespondTitle != null && i.RespondTitle != "Yardımcı Admin" && i.RespondTitle != "Asistan Admin").OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyDetail(string id)
        {
            return View(_userService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult UserSetting()
        {
            return View(_userService.GetAll().Where(i => i.IsConfirmed == true && i.RespondTitle != "Admin" && i.RespondTitle != "Yardımcı Admin" && i.RespondTitle != "Asistan Admin" && i.RespondTitle == null).OrderByDescending(i => i.CreatedDate).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _Information(string userId)
        {
            userId = Convert.ToString(Session["adminId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LongInformation(string userId)
        {
            userId = Convert.ToString(Session["adminId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastCommentList()
        {
            return PartialView(_commentService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastComplainList()
        {
            return PartialView(_complainService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastUserList()
        {
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastCompanyList()
        {
            return PartialView(_companyService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastProductList()
        {
            return PartialView(_productService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastOrderList()
        {
            return PartialView(_orderService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _LastCancelRequestList()
        {
            return PartialView(_cancelService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).Take(10).ToList());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _Navbar()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _Header()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _Footer()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _Search()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SearchResult(string key, int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.NameLastname.Contains(key) || i.UserName.Contains(key) || i.Province.Contains(key) || i.City.Contains(key) || i.Country.Contains(key)).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult SetCookie()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetCookie(string CookieName, string CookieValue)
        {
            HttpCookie ck = new HttpCookie(CookieName);
            ck.Value = CookieValue;
            ck.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(ck);
            return View();
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ReadCookie()
        {
            string deger = Request.Cookies["ocL"].Value;
            ViewBag.Cerez = deger;
            return View();
        }
        public ActionResult DeleteCookie(string name)
        {
            Response.Cookies.Remove(name);
            Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction(nameof(ReadCookie));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(string id)
        {
            var deleteUser = _userService.GetById(id);
            if (deleteUser != null)
            {
                _userService.Delete(deleteUser);
            }
            return RedirectToAction(nameof(AdminList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(string id)
        {
            _userService.SetActive(id);
            return RedirectToAction(nameof(AdminList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(string id)
        {
            _userService.SetDeActive(id);
            return RedirectToAction(nameof(AdminList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteUser(string id)
        {
            var deleteUser = _userService.GetById(id);
            if (deleteUser != null)
            {
                _userService.Delete(deleteUser);
            }
            return RedirectToAction(nameof(UserList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveUser(string id)
        {
            _userService.SetActive(id);
            return RedirectToAction(nameof(UserList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveUser(string id)
        {
            _userService.SetDeActive(id);
            return RedirectToAction(nameof(UserList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteCompany(string id)
        {
            var deleteUser = _userService.GetById(id);
            if (deleteUser != null)
            {
                _userService.Delete(deleteUser);
            }
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveCompany(string id)
        {
            _userService.SetActive(id);
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveCompany(string id)
        {
            _userService.SetDeActive(id);
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteSetting(string id)
        {
            var deleteUser = _userService.GetById(id);
            if (deleteUser != null)
            {
                _userService.Delete(deleteUser);
            }
            return RedirectToAction(nameof(UserSetting));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveSetting(string id)
        {
            _userService.SetActive(id);
            return RedirectToAction(nameof(UserSetting));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveSetting(string id)
        {
            _userService.SetDeActive(id);
            return RedirectToAction(nameof(UserSetting));
        }
    }
}