using OrderFood.Entity.Models;
using OrderFood.Service.OrderService;
using OrderFood.Service.PaymentService;
using PagedList;
using OrderFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderFood.Service.ProductService;
using OrderFood.Service.ApplicationUserService;

namespace OrderFood.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IProductService _productService;
        private readonly IApplicationUserService _userService;
        public OrderController(IOrderService orderService, IPaymentService paymentService, IProductService productService, IApplicationUserService userserVice)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _productService = productService;
            _userService = userserVice;
        }

        [Audit]
        public ActionResult _HitCount(int? id)
        {
            return PartialView(_orderService.HitRead(id));
        }
        [Audit]
        public ActionResult TotalOrderList()
        {
            ViewBag.Sepets = Sepet.AktifSepet.ToplamTutar.ToString();
            List<SepetItem> products = new List<SepetItem>();
            if (Session["AktifSepet"] != null)
            {
                Sepet spt = (Sepet)Session["AktifSepet"];
                products = spt.Urunler;
            }
            return View(products);
        }
        [Audit]
        public ActionResult Pay()
        {
            ViewBag.Sepets = Sepet.AktifSepet.ToplamTutar.ToString();
            List<SepetItem> products = new List<SepetItem>();
            if (Session["AktifSepet"] != null)
            {
                Sepet spt = (Sepet)Session["AktifSepet"];
                products = spt.Urunler;
            }
            return View(products);
        }
        [Audit]
        public ActionResult LessPiece(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];
                if (s.Urunler.FirstOrDefault(i => i.Urun.Id == id).Adet > 1)
                {
                    s.Urunler.FirstOrDefault(i => i.Urun.Id == id).Adet--;
                }
                else
                {
                    SepetItem spt = s.Urunler.FirstOrDefault(i => i.Urun.Id == id);
                    s.Urunler.Remove(spt);
                }
            }
            return RedirectToAction(nameof(Pay));
        }
        [Audit]
        public ActionResult MorePiece(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];

                if (s.Urunler.FirstOrDefault(i => i.Urun.Id == id).Adet >= 0)
                {
                    s.Urunler.FirstOrDefault(i => i.Urun.Id == id).Adet++;
                }
                else
                {
                    SepetItem spt = s.Urunler.FirstOrDefault(i => i.Urun.Id == id);
                    s.Urunler.Remove(spt);
                }
            }
            return RedirectToAction(nameof(Pay));
        }

        [Audit]
        [Authorize(Roles = "Users,Tienda,Admin,Asistant,Helper")]
        public ActionResult CreatePay()
        {
            ViewBag.Payments = _paymentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToList();
            return View();
        }

        [Audit]
        [Authorize(Roles = "Users,Tienda,Admin,Asistant,Helper")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePay(Order model)
        {
            List<SepetItem> products = new List<SepetItem>();
            if (Session["AktifSepet"] != null)
            {
                Sepet spt = (Sepet)Session["AktifSepet"];
                products = spt.Urunler;
                foreach (var item in products)
                {
                    model.Piece = Convert.ToInt32(item.Adet);
                    model.SellingPrice = Convert.ToDecimal(item.ToplamFiyat);
                    model.Discount = Convert.ToDouble(item.Indirim);
                    model.TotalPrice = Convert.ToDecimal(Sepet.AktifSepet.ToplamTutar);

                    if (item.UserId != null)
                    {
                        model.UserId = item.UserId;
                    }
                    model.CompanyId = item.CompanyId;
                    if (item.ProductId != null)
                    {
                        model.ProductId = item.ProductId;
                    }
                    _orderService.Create(model);
                }
            }
            return RedirectToAction(nameof(TotalOrderList));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult OrderDetail(int? id)
        {
            return View(_orderService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult PaymentList(int? id, int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.PaymentId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyList(int? id, int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult OrderList(int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult OrderUsers(int? id, int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 100));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult TotalOrderUser(int page = 1)
        {
            return View(_orderService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 100));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult UserList(string key, int page = 1)
        {
            return View(_userService.GetAll().Where(i => i.UserName.Contains(key) || i.NameLastname.Contains(key)).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 100));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _orderService.SetActive(id);
            return RedirectToAction(nameof(OrderList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _orderService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteOrder = _orderService.GetById(id);
            if (deleteOrder != null)
            {
                _orderService.Delete(deleteOrder);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult _SearchUser()
        {
            return PartialView();
        }
    }
}