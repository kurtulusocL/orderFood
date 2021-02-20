using Newtonsoft.Json;
using OrderFood.Entity.Models;
using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.CommentService;
using OrderFood.Service.ComplainService;
using OrderFood.Service.ProductDetailService;
using OrderFood.Service.ProductService;
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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        private readonly IComplainService _complainService;
        private readonly IProductDetailService _productDetailService;
        public ProductController(IProductService productService, ICommentService commentService, IComplainService complainService, IProductDetailService productDetailService)
        {
            _productService = productService;
            _commentService = commentService;
            _complainService = complainService;
            _productDetailService = productDetailService;
        }
        [Audit]
        public ActionResult Index(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 27));
        }

        [Audit]
        public ActionResult Detail(int? id)
        {
            return View(_productService.GetById(id));
        }

        [Audit]
        public void AddtoCard(int id)
        {           
            SepetItem spt = new SepetItem();
            Product u = _productService.GetAllIncluding().FirstOrDefault(i => i.Id == id);

            try
            {
                spt.ProductId = id;
                spt.CompanyId = u.CompanyId;
                spt.UserId = User.Identity.Name;
                spt.Urun = u;
                spt.Adet = 1;
                spt.Indirim = 0;

                Sepet s = new Sepet();
                s.SepeteEkle(spt);
                TempData["successresult"] = "Producto añadido al carrito";
            }
            catch (Exception)
            {
                TempData["errorresult"] = "El producto no se pudo agregar al carrito.";
            }
        }
        [Audit]
        public ActionResult Like(int? id, Product model)
        {
            var like = _productService.Like(id);
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Detail", controller = "Product", id = model.Id }));
        }

        [Audit]
        public ActionResult _HitCount(int? id)
        {
            return PartialView(_productService.HitRead(id));
        }

        [Audit]
        public ActionResult Category(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CategoryId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 27));
        }

        [Audit]
        public ActionResult Subcategory(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.SubcategoryId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 27));
        }

        [Audit]
        public ActionResult Country(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 27));
        }

        [Audit]
        public ActionResult City(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 27));
        }

        [Audit]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 27));
        }

        [Audit]
        public ActionResult _CreateComment(int? id)
        {
            Session["productId"] = id;
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
                    model.ProductId = Convert.ToInt32(Session["productId"]);
                    _commentService.Create(model);
                    TempData["message"] = "Su comentario ha sido agregado con éxito.";
                }
                if (User.IsInRole("Users") && User.Identity.IsAuthenticated == true)
                {
                    model.UserId = Convert.ToString(Session["userId"]);
                    model.ProductId = Convert.ToInt32(Session["productId"]);
                    _commentService.Create(model);
                    TempData["message"] = "Su comentario ha sido agregado con éxito.";
                }
                if (User.Identity.IsAuthenticated == false)
                {
                    model.ProductId = Convert.ToInt32(Session["productId"]);
                    _commentService.Create(model);
                    TempData["message"] = "Su comentario ha sido agregado con éxito.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Recibimos un error al guardar tu comentario. Verifique los campos relevantes y comente nuevamente.";
                return RedirectToAction(nameof(Detail));
            }  
            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Detail", controller = "Product", id = model.ProductId }));
        }

        [Audit]
        public ActionResult Complain(int? id)
        {
            Session["productId"] = id;
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

                model.ProductId = Convert.ToInt32(Session["productId"]);
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

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ProductDetail(int id)
        {
            return View(_productService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ProductInfo(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.ProductDetailId == id).FirstOrDefault());
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CategoryList(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CategoryId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult SubcategoryList(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.SubcategoryId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CountryList(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CityList(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyList(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ProductList(int page = 1)
        {
            return View(_productService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _productService.SetActive(id);
            return RedirectToAction(nameof(ProductList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _productService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveDetail(int id)
        {
            Product model = new Product();
            _productDetailService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.ProductDetailId });
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveDetail(int id)
        {
            Product model = new Product();
            _productDetailService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.ProductDetailId });
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteProduct = _productService.GetById(id);
            if (deleteProduct != null)
            {
                _productService.Delete(deleteProduct);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteDetail(int id)
        {
            var deleteDetail = _productDetailService.GetById(id);
            if (deleteDetail != null)
            {
                _productDetailService.Delete(deleteDetail);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}