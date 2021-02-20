using OrderFood.Entity.Models;
using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.CancelRequestService;
using OrderFood.Service.CityService;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Service.CountryService;
using OrderFood.Service.KindService;
using OrderFood.Service.MessageForUserService;
using OrderFood.Service.MessageFromCompanyService;
using OrderFood.Service.OrderService;
using OrderFood.Service.PaymentService;
using OrderFood.Service.PictureService;
using OrderFood.Service.ProductDetailService;
using OrderFood.Service.ProductService;
using OrderFood.Service.ProfilePhotoService;
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
    [Audit]
    [Authorize(Roles = "Admin,Tienda")]
    public class HomeCompanyController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly ICompanyService _companyService;
        private readonly IProductService _productService;
        private readonly IMessageForUserService _messageService;
        private readonly ICompanySocialMediaService _companySocialMediaService;
        private readonly ICompanyContactService _companyContactService;
        private readonly IKindService _kindService;
        private readonly IPictureService _pictureService;
        private readonly ICommentService _commentService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IMessageFromCompanyService _companyMessageService;
        private readonly ICancelRequestService _cancelRequestService;
        private readonly IProductDetailService _productDetailService;
        private readonly IProfilePhotoService _photoService;
        public HomeCompanyController(IApplicationUserService userService, ICompanyService companyService, IProductService productService, IMessageForUserService messageService, ICompanySocialMediaService companySocialMediaService, ICompanyContactService companyContactService, IKindService kindService, IPictureService pictureService, ICommentService commentService, ICountryService countryService, ICityService cityService, IPaymentService paymentService, IOrderService orderService, IMessageFromCompanyService companyMessageService, ICancelRequestService cancelRequestService, IProductDetailService productDetailService, IProfilePhotoService photoService)
        {
            _userService = userService;
            _companyService = companyService;
            _productService = productService;
            _messageService = messageService;
            _companySocialMediaService = companySocialMediaService;
            _companyContactService = companyContactService;
            _kindService = kindService;
            _pictureService = pictureService;
            _commentService = commentService;
            _countryService = countryService;
            _cityService = cityService;
            _paymentService = paymentService;
            _orderService = orderService;
            _companyMessageService = companyMessageService;
            _cancelRequestService = cancelRequestService;
            _productDetailService = productDetailService;
            _photoService = photoService;
        }
        public ActionResult Index(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction(nameof(CompanyAccountController.Login));
            if (User.Identity.IsAuthenticated == true)
            {
                var userHome = _userService.GetAll().Where(i => i.Id == userId && i.IsConfirmed == true).ToList();
                return View(userHome);
            }
            return View();
        }
        public ActionResult _UserInformationHome(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }
        public ActionResult _ShortInformation(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }
        public ActionResult _ContactHome(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }
        public ActionResult _LocationHome(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }
        public ActionResult _UserAttribute(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).FirstOrDefault());
        }
        public ActionResult _ProfilePhotoHome(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_photoService.GetAll().Where(i => i.IsConfirmed == true && i.UserId == userId).SingleOrDefault());
        }
        public ActionResult _ProfilePhotoHeader(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_photoService.GetAll().Where(i => i.IsConfirmed == true && i.UserId == userId).FirstOrDefault());
        }
        public ActionResult _Navbar()
        {
            return PartialView();
        }
        public ActionResult _HeaderNotification()
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
        public ActionResult _OrderCompany()
        {
            return PartialView(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true).FirstOrDefault());
        }
        public ActionResult _OrderCompanySent()
        {
            return PartialView(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true).FirstOrDefault());
        }
        public ActionResult _CompanyIstatisticHome(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _ProductHome(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _ProductPhotoHome(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _CompanyProductCountry()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Name).ToList());
        }
        public ActionResult _CompanyCountryIstatisticHome(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.CountryId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _CompanyProductCity()
        {
            return PartialView(_cityService.GetAllIncluding().OrderBy(i => i.Name).ToList());
        }
        public ActionResult _CompanyCityIstatisticHome(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.CityId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _CompanyProfile(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_photoService.GetAll().Where(i => i.IsConfirmed == true && i.UserId == userId).FirstOrDefault());
        }
        public ActionResult _ProfilePhoto(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_photoService.GetAll().Where(i => i.IsConfirmed == true && i.UserId == userId).SingleOrDefault());
        }
        public ActionResult _CompanyList(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult CompanyProductList(int? id, string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult CompanyCommentIndex(int? id, int page = 1)
        {
            return View(_commentService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
        }
        public ActionResult ProductCommentIndex(int? id, int page = 1)
        {
            return View(_commentService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 25));
        }
        public ActionResult YourCompanyList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult YourProductList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult YourProductDetailList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productDetailService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }
        public ActionResult YourPhotoList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 45));
        }
        public ActionResult YourCompanyPhotoList(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 45));
        }
        public ActionResult YourProductPhotoList(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 45));
        }
        public ActionResult CityList()
        {
            return View(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult CountryList()
        {
            return View(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult RivalMostExpensiveProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.SellingPrice).Take(50).ToList());
        }
        public ActionResult CountryRivalMostExpensiveProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.SellingPrice).Take(70).ToList());
        }
        public ActionResult RivalMostCheaperProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderBy(i => i.SellingPrice).Take(50).ToList());
        }
        public ActionResult CountryRivalMostCheaperProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderBy(i => i.SellingPrice).Take(70).ToList());
        }
        public ActionResult RivalMostLikedProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Like.Value > 0).OrderByDescending(i => i.Like).Take(50).ToList());
        }
        public ActionResult CountryRivalMostLikedProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Like.Value > 0).OrderByDescending(i => i.Like).Take(70).ToList());
        }
        public ActionResult RivalLittleLikedProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderBy(i => i.Like).Take(50).ToList());
        }
        public ActionResult CountryLittleLikedProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderBy(i => i.Like).Take(70).ToList());
        }
        public ActionResult RivalMostOrderProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMostOrderProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalLessOrderProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalLessOrderProduct(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalMostExpensiveMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Orders.Count() > 0).OrderByDescending(i => i.SellingPrice).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMostExpensiveMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Orders.Count() > 0).OrderByDescending(i => i.SellingPrice).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalMostCheapLesSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Orders.Count() > 0).OrderBy(i => i.SellingPrice).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMostCheapLesSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Orders.Count() > 0).OrderBy(i => i.SellingPrice).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalMostLikedMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Orders.Count() > 0 && i.Like.Value > 0).OrderByDescending(i => i.Like).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMostLikedMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Orders.Count() > 0 && i.Like.Value > 0).OrderByDescending(i => i.Like).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalMostLikedLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Like.Value > 0).OrderByDescending(i => i.Like).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMostLikedLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Like.Value > 0).OrderByDescending(i => i.Like).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalLessLikedMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Orders.Count() > 0).OrderBy(i => i.Like).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalLessLikedMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Orders.Count() > 0).OrderBy(i => i.Like).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalLessLikedLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderBy(i => i.Like).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalLessLikedLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderBy(i => i.Like).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalLessHitLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderBy(i => i.Hit).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalLessHitLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderBy(i => i.Hit).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalLessHitMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Orders.Count() > 0).OrderBy(i => i.Hit).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalLessHitMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Orders.Count() > 0).OrderBy(i => i.Hit).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalMoreHitLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Hit > 0).OrderByDescending(i => i.Hit).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMoreHitLessSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Hit > 0).OrderByDescending(i => i.Hit).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult RivalMoreHitMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id && i.Hit > 0 && i.Orders.Count() > 0).OrderByDescending(i => i.Hit).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult CountryRivalMoreHitMoreSell(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id && i.Hit > 0 && i.Orders.Count() > 0).OrderByDescending(i => i.Hit).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult PaymentList()
        {
            return View(_paymentService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Orders.Count() > 0).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult PaymentMostOrder()
        {
            return View(_paymentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToList());
        }
        public ActionResult PaymentLessOrder()
        {
            return View(_paymentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Orders.Count()).ToList());
        }
        public ActionResult YourMostSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Orders.Count() > 0 && i.UserId == userId).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult YourLessSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult YouMostLiked(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Like > 0 && i.UserId == userId).OrderByDescending(i => i.Like).Take(50).ToList());
        }
        public ActionResult YouLessLiked(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Like).Take(50).ToList());
        }
        public ActionResult YourMostSaveMoney(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Orders.Count() > 0).OrderByDescending(i => i.SellingPrice * i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult YourMostSellCompany(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Orders.Count() > 0 && i.UserId == userId).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult YourExpensiveMostSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Orders.Count() > 0 && i.UserId == userId).OrderByDescending(i => i.Orders.Count() & Convert.ToInt32(i.SellingPrice)).Take(50).ToList());
        }
        public ActionResult YourCheapLessSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Orders.Count() & Convert.ToInt32(i.SellingPrice)).Take(50).ToList());
        }
        public ActionResult YourMostLikeMoreSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Like > 0 && i.Orders.Count() > 0).OrderByDescending(i => i.Orders.Count() & i.Like).Take(50).ToList());
        }
        public ActionResult YourMostLikeLessSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Like > 0).OrderByDescending(i => i.Like).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult YourLessLikeMoreSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Orders.Count() > 0).OrderBy(i => i.Like).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult YourLessLikeLessSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Like & i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult LessHitLessSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Hit & i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult LessHitMoreSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Orders.Count() > 0).OrderBy(i => i.Hit).OrderByDescending(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult MoreHitLessSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Hit > 0).OrderByDescending(i => i.Hit).OrderBy(i => i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult MoreHitMoreSell(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId && i.Hit > 0 && i.Orders.Count() > 0).OrderByDescending(i => i.Hit & i.Orders.Count()).Take(50).ToList());
        }
        public ActionResult FromExpensiveToCheap(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.SellingPrice).ToPagedList(page, 40));
        }
        public ActionResult FromCheapToExpensive(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.SellingPrice).ToPagedList(page, 40));
        }
        public ActionResult MostLikeProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.Like).ToPagedList(page, 40));
        }
        public ActionResult LessLikeProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Like).ToPagedList(page, 40));
        }
        public ActionResult MoreSellingProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult LessSellingProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult MoreHitProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.Hit).ToPagedList(page, 40));
        }
        public ActionResult LessHitProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderBy(i => i.Hit).ToPagedList(page, 40));
        }
        public ActionResult YourMessageBox(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_messageService.GetAll().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult SendAnswer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAnswer(MessageFromCompany model)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("kurtulusocal@yandex.com");
                mail.Priority = MailPriority.High;
                mail.Subject = model.Subject;
                mail.To.Add(new MailAddress(model.Reciever, ""));
                mail.Body = model.Text;
                mail.IsBodyHtml = true;

                NetworkCredential enter = new NetworkCredential("emailaddress", "password");
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = enter;
                client.Send(mail);

                _companyMessageService.Create(model);
                TempData["message"] = "La operación de envío de mensajes se realiza correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se ha producido un error al enviar el mensaje. Por favor, compruebe los lugares relevantes y envíelos de vuelta.";
                return RedirectToAction(nameof(SendAnswer));
            }
            return RedirectToAction(nameof(SendAnswer));
        }
        public ActionResult UserProfile(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_userService.GetAll().Where(i => i.IsConfirmed == true && i.Id == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult UnConfirmCompany(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult UnConfirmPhotos(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.UserId == userId).OrderByDescending(i => i.CreatedDate.ToLocalTime()));
        }
        public ActionResult UnConfirmCompanyPhotos(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate.ToLocalTime()));
        }
        public ActionResult UnConfirmProduct(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult UnConfirmProductPhotos(string userId, int? id)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate.ToLocalTime()));
        }
        public ActionResult _GetUserId(string userId)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return PartialView(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).FirstOrDefault());
        }
        public ActionResult SendCancel(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendCancel(CancelRequest model)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);
                _cancelRequestService.Create(model);
                TempData["message"] = "Su solicitud de cancelación de membresía se ha enviado correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se ha producido un error al enviar la solicitud de cancelación de la suscripción. Por favor, compruebe los lugares relevantes y envíelos de vuelta.";
                return RedirectToAction(nameof(SendCancel));
            }
            return RedirectToAction(nameof(SendCancel));
        }
        public ActionResult OrderList(int? id, int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id && i.IsSent == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }
        public ActionResult PasifOrderList(int? id, int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == false && i.IsSent == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }
        public ActionResult MyCompanyList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 35));
        }
        public ActionResult HowCreate()
        {
            return View();
        }
    }
}