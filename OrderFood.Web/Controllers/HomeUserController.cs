using OrderFood.Entity.Models;
using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.CancelRequestService;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.ComplainService;
using OrderFood.Service.MessageForUserService;
using OrderFood.Service.OrderService;
using OrderFood.Service.PictureService;
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
    [Authorize(Roles = "Admin,Users")]
    public class HomeUserController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly IProductService _productService;
        private readonly ICompanyService _companyService;
        private readonly IPictureService _pictureService;
        private readonly ICommentService _commentService;
        private readonly IComplainService _complainService;
        private readonly IMessageForUserService _messageService;
        private readonly ICancelRequestService _cancelService;
        private readonly IOrderService _orderService;
        public HomeUserController(IApplicationUserService userService, IProductService productService, ICompanyService companyService, IPictureService pictureService, ICommentService commentService, IComplainService complainService, IMessageForUserService messageService, ICancelRequestService cancelService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _companyService = companyService;
            _commentService = commentService;
            _complainService = complainService;
            _pictureService = pictureService;
            _messageService = messageService;
            _cancelService = cancelService;
            _orderService = orderService;
        }
        public ActionResult Index(string userId)
        {
            userId = Convert.ToString(Session["userId"]);
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("Login", "UserAccount");
            if (User.Identity.IsAuthenticated == true)
            {
                var userHome = _userService.GetAll().Where(i => i.Id == userId).ToList();
                return View(userHome);
            }
            return View();
        }
        public ActionResult YourOrder(int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == User.Identity.Name).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult OrderProduct(int? id, int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == User.Identity.Name && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult YourComment(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_commentService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
        }
        public ActionResult CommentCompany(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_commentService.GetAllIncluding().Where(i => i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
        }
        public ActionResult CommentProduct(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_commentService.GetAllIncluding().Where(i => i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
        }
        public ActionResult YourComplain(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_complainService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }
        public ActionResult ComplainCompany(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_complainService.GetAllIncluding().Where(i => i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }
        public ActionResult ComplainProduct(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_complainService.GetAllIncluding().Where(i => i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 20));
        }
        public ActionResult MostPopularCompany(string cityName)
        {
            cityName = Convert.ToString(Session["cityName"]);
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.City.Name == cityName && i.Orders.Count() > 0).OrderByDescending(i => i.Orders.Count()).Take(56).ToList());
        }
        public ActionResult MostPopularProduct(string cityName)
        {
            cityName = Convert.ToString(Session["cityName"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.City.Name == cityName && i.Orders.Count() > 0).OrderByDescending(i => i.Orders.Count()).Take(56).ToList());
        }
        public ActionResult OtherPeopleBuy(string cityName)
        {
            cityName = Convert.ToString(Session["cityName"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.City.Name == cityName && i.Orders.Count() > 0).OrderByDescending(i => i.Orders.Count()).Take(40));
        }
        public ActionResult YourMessage(string userId)
        {
            userId = Convert.ToString(Session["userId"]);
            return View(_messageService.GetAll().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult SendCancelRequest(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendCancelRequest(CancelRequest model)
        {
            try
            {
                model.UserId = Convert.ToString(Session["userId"]);
                _cancelService.Create(model);
                TempData["message"] = "Su solicitud de cancelación de suscripción se ha enviado correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al enviar la solicitud de cancelación de suscripción. Compruebe los lugares relevantes y vuelva a intentarlo.";
                return RedirectToAction(nameof(SendCancelRequest));
            }
            return RedirectToAction(nameof(SendCancelRequest));
        }
        public ActionResult _Navbar()
        {
            return PartialView();
        }
        public ActionResult _HeaderNavbar()
        {
            return PartialView();
        }
        public ActionResult _HeaderAccount()
        {
            return PartialView();
        }
        public ActionResult _ProfilePhotoHeader()
        {
            return PartialView();
        }
        public ActionResult _ProfileInformation(string userId)
        {
            userId = Convert.ToString(Session["userId"]);
            return PartialView(_userService.GetAll().Where(i => i.Id == userId && i.IsConfirmed == true).FirstOrDefault());
        }
        public ActionResult _ProfileOrder()
        {
            return PartialView(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == User.Identity.Name).OrderByDescending(i => i.CreatedDate).Take(20).ToList());
        }
        public ActionResult _ProfileDeliveryOrder()
        {
            return PartialView(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.UserId == User.Identity.Name).OrderByDescending(i => i.CreatedDate).Take(20).ToList());
        }
        public ActionResult _ProfilePeopleChoise()
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Orders.Count() > 0).OrderByDescending(i => i.Orders.Count()).Take(12).ToList());
        }
        public ActionResult _ProfilePeopleChoisePhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _MostPopularProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _OtherPeopleBuyPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _GetUserId(string userId)
        {
            userId = Convert.ToString(Session["userId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }
    }
}