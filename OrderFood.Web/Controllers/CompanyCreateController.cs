using OrderFood.Entity.Models;
using OrderFood.Service.CategoryService;
using OrderFood.Service.CityService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Service.CountryService;
using OrderFood.Service.KindService;
using OrderFood.Service.OrderService;
using OrderFood.Service.PictureService;
using OrderFood.Service.ProductDetailService;
using OrderFood.Service.ProductService;
using OrderFood.Service.ProfilePhotoService;
using OrderFood.Service.SubcategoryService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    [Audit]
    [Authorize(Roles = "Tienda")]
    public class CompanyCreateController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyContactService _companyContactService;
        private readonly ICompanySocialMediaService _companySocialMediaService;
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private readonly IPictureService _pictureService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IKindService _kindService;
        private readonly IOrderService _orderService;
        private readonly IProfilePhotoService _photoService;
        public CompanyCreateController(ICompanyService companyService, ICompanyContactService companyContactService, ICompanySocialMediaService companySocialMediaService, IProductService productService, IProductDetailService productDetailService, IPictureService pictureService, ICategoryService categoryService, ISubcategoryService subcategoryService, ICountryService countryService, ICityService cityService, IKindService kindService, IOrderService orderService, IProfilePhotoService photoService)
        {
            _companyService = companyService;
            _companyContactService = companyContactService;
            _companySocialMediaService = companySocialMediaService;
            _productService = productService;
            _productDetailService = productDetailService;
            _pictureService = pictureService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _countryService = countryService;
            _cityService = cityService;
            _kindService = kindService;
            _orderService = orderService;
            _photoService = photoService;
        }
        public ActionResult CompanyList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }
        public ActionResult ProductList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }
        public ActionResult CompanyContactList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyContactService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult CompanySocialMediaList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companySocialMediaService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult PictureList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 60));
        }
        public ActionResult CompanyPictureList(int? id, string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }
        public ActionResult ProductPictureList(int? id, string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }
        public ActionResult ProductDetail(int? id)
        {
            return View(_productService.GetById(id));
        }
        public ActionResult ProductInfo(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.ProductDetailId == id).FirstOrDefault());
        }
        public ActionResult CompanyDetail(int? id)
        {
            return View(_companyService.GetById(id));
        }

        [HttpPost]
        public JsonResult SelectList(int? countryId, int? categoryId, string tip)
        {
            var countryList = _countryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();
            var cityList = _cityService.GetAllIncluding().Where(i => i.CountryId == countryId && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();
            var categoryList = _categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();
            var subcategoryList = _subcategoryService.GetAllIncluding().Where(i => i.CategoryId == categoryId && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();

            List<SelectListItem> sonuc = new List<SelectListItem>();
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "GetCountry":
                        foreach (var item in countryList)
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString()
                            });
                        }
                        break;
                    case "GetCity":
                        foreach (var item in cityList)
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString()
                            });
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });
            }

            List<SelectListItem> result = new List<SelectListItem>();
            bool isOke = true;
            try
            {
                switch (tip)
                {
                    case "GetCategory":
                        foreach (var item in categoryList)
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString()
                            });
                        }
                        break;
                    case "GetSubcategory":
                        foreach (var item in subcategoryList)
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString()
                            });
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                isOke = false;
                result = new List<SelectListItem>();
                result.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });
            }
            return Json(new { ok = basariliMi, text = sonuc });
        }
        public ActionResult ProfilePhotoCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePhotoCreate(ProfilePhoto model, HttpPostedFileBase image)
        {
            try
            {
                model.UserId = Convert.ToString(Session["TiendaId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/profile/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _photoService.Create(model);
                TempData["message"] = "Su foto de perfil se ha añadido correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se ha producido un error al insertar la imagen de perfil.";
                return RedirectToAction(nameof(ProfilePhotoCreate));
            }
            return RedirectToAction("Index", "HomeCompany");
        }
        public ActionResult ProfilePhotoEdit(int? id)
        {
            var updatePhoto = _photoService.GetById(id);
            if (updatePhoto != null)
            {
                return View(updatePhoto);
            }
            return RedirectToAction("Index", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePhotoEdit(ProfilePhoto model, HttpPostedFileBase image)
        {
            try
            {
                model.UserId = Convert.ToString(Session["TiendaId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/profile/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _photoService.Update(model);
                TempData["message"] = "Su foto de perfil se ha actualizado correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se ha producido un error al actualizar la imagen de perfil.";
                return RedirectToAction(nameof(ProfilePhotoEdit));
            }
            return RedirectToAction("Index", "HomeCompany");
        }
        public ActionResult CompanyCreate()
        {
            ViewBag.Kinds = _kindService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Companies.Count()).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCreate(Company model, HttpPostedFileBase image)
        {
            try
            {
                model.UserId = Convert.ToString(Session["TiendaId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/logo/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companyService.Create(model);
                TempData["message"] = "El proceso de adición de su compañía es exitoso.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se ha producido un error mientras se agregaba su compañía.";
                return RedirectToAction(nameof(CompanyCreate));
            }
            return RedirectToAction(nameof(CompanyCreate));
        }
        public ActionResult CompanyEdit(int id)
        {
            ViewBag.Kinds = _kindService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Companies.Count()).ToList();

            var updateCompany = _companyService.GetById(id);
            if (updateCompany != null)
            {
                return View(updateCompany);
            }
            return RedirectToAction(nameof(CompanyList));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyEdit(Company model, HttpPostedFileBase image)
        {
            try
            {
                model.UserId = Convert.ToString(Session["TiendaId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/logo/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companyService.Update(model);
                TempData["message"] = "La actualización de su compañía es un éxito.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar su compañía.";
                return RedirectToAction(nameof(CompanyEdit));
            }
            return RedirectToAction(nameof(CompanyList));
        }
        public ActionResult SocialMediaCreate(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SocialMediaCreate(CompanySocialMedia model, HttpPostedFileBase image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/companysocial/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companySocialMediaService.Create(model);
                TempData["message"] = "Agregar cuentas de redes sociales es un éxito.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al agregar cuentas de redes sociales.";
                return RedirectToAction(nameof(SocialMediaCreate));
            }
            return RedirectToAction(nameof(SocialMediaCreate));
        }
        public ActionResult SocialMediaEdit(int? id)
        {
            Session["companyId"] = id;
            var updateSocialMedia = _companySocialMediaService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            if (updateSocialMedia != null)
            {
                return View(updateSocialMedia);
            }
            return RedirectToAction(nameof(CompanySocialMediaList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SocialMediaEdit(CompanySocialMedia model, HttpPostedFileBase image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/companysocial/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companySocialMediaService.Update(model);
                TempData["message"] = "La actualización de las cuentas de redes sociales es un éxito.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar las cuentas de redes sociales.";
                return RedirectToAction(nameof(SocialMediaEdit));
            }
            return RedirectToAction(nameof(SocialMediaEdit));
        }
        public ActionResult ContactCreate(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactCreate(CompanyContact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["TiendaId"]);
                    _companyContactService.Create(model);
                    TempData["message"] = "Agregar las direcciones de contacto de su compañía es un éxito.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al agregar las direcciones de contacto de su compañía.";
                return RedirectToAction(nameof(ContactCreate));
            }
            return RedirectToAction(nameof(ContactCreate));
        }
        public ActionResult ContactEdit(int? id)
        {
            Session["companyId"] = id;
            var updateContact = _companyContactService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            if (updateContact != null)
            {
                return View(updateContact);
            }
            return RedirectToAction(nameof(CompanyContactList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactEdit(CompanyContact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["TiendaId"]);
                    _companyContactService.Update(model);
                    TempData["message"] = "La actualización de las direcciones de contacto de su compañía se ha realizado correctamente.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar las direcciones de contacto de su compañía.";
                return RedirectToAction(nameof(ContactEdit));
            }
            return RedirectToAction(nameof(ContactEdit));
        }
        public ActionResult ProductCreate(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["TiendaId"]);
                    model.ProductDetail.UserId = Convert.ToString(Session["TiendaId"]);
                    _productService.Create(model);
                    TempData["message"] = "El proceso de agregar el producto de su compañía es exitoso.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al agregar el producto de su compañía.";
                return RedirectToAction(nameof(ProductCreate));
            }
            return RedirectToAction(nameof(ProductCreate));
        }
        public ActionResult ProductEdit(int? id)
        {
            Session["companyId"] = id;
            var updateProduct = _productService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            if (updateProduct != null)
            {
                return View(updateProduct);
            }
            return RedirectToAction(nameof(ProductList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["TiendaId"]);
                    model.ProductDetail.UserId = Convert.ToString(Session["TiendaId"]);
                    _productService.Update(model);
                    TempData["message"] = "El proceso de actualización del producto de su compañía es exitoso.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar el producto de su compañía.";
                return RedirectToAction(nameof(ProductEdit));
            }
            return RedirectToAction(nameof(ProductEdit));
        }
        public ActionResult ProductPriceEdit(int? id)
        {
            Session["companyId"] = id;
            var updateProduct = _productService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            var model = new ProductEditModel()
            {
                Id = updateProduct.Id,
                Price = updateProduct.SellingPrice
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductPriceEdit(ProductEditModel model)
        {
            try
            {
                var product = _productService.GetById(model.Id);
                product.SellingPrice = model.Price;
                _productService.Update(product);
                TempData["message"] = "La actualización del precio del producto se ha realizado correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar el precio del producto.";
                return RedirectToAction(nameof(ProductPriceEdit));
            }
            return RedirectToAction(nameof(ProductPriceEdit));
        }
        public ActionResult ProductDiscountEdit(int? id)
        {
            Session["companyId"] = id;
            var updateProduct = _productService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            var model = new ProductEditDiscount()
            {
                Id = updateProduct.Id,
                Discount = Convert.ToDecimal(updateProduct.DicsountPrice)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDiscountEdit(ProductEditDiscount model)
        {
            try
            {
                var product = _productService.GetById(model.Id);
                product.DicsountPrice = model.Discount;
                _productService.Update(product);
                TempData["message"] = "La actualización del precio de descuento de su producto se realizó correctamente.";
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar el precio de descuento de su producto.";
                return RedirectToAction(nameof(ProductDiscountEdit));
            }
            return RedirectToAction(nameof(ProductDiscountEdit));
        }
        public ActionResult CompanyPhotoCreate(int? id)
        {
            Session["companyId"] = id;
            var photo = _pictureService.GetAllIncluding().FirstOrDefault(i => i.CompanyId == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyPhotoCreate(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    _pictureService.Create(model);
                    TempData["message"] = "La adición de imágenes de la compañía se realizó correctamente.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al agregar imágenes de la compañía.";
                return RedirectToAction(nameof(CompanyPhotoCreate));
            }
            return RedirectToAction(nameof(CompanyPhotoCreate));
        }
        public ActionResult CompanyPhotoEdit(int? id)
        {
            Picture model = new Picture();
            model.CompanyId = Convert.ToInt32(Session["companyId"]);
            model.UserId = Convert.ToString(Session["TiendaId"]);

            var updatePhoto = _pictureService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            if (updatePhoto != null)
            {
                return View(updatePhoto);
            }
            return RedirectToAction(nameof(PictureList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyPhotoEdit(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    _pictureService.Update(model);
                    TempData["message"] = "La actualización de las imágenes de la compañía se ha realizado correctamente.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar las imágenes de la compañía.";
                return RedirectToAction(nameof(CompanyPhotoEdit));
            }
            return RedirectToAction(nameof(CompanyPhotoEdit));
        }
        public ActionResult ProductPhotoCreate(int? id)
        {
            Session["productId"] = id;
            var photo = _pictureService.GetAllIncluding().FirstOrDefault(i => i.ProductId == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductPhotoCreate(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.ProductId = Convert.ToInt32(Session["productId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    model.ProductId = Convert.ToInt32(Session["productId"]);
                    _pictureService.Create(model);
                    TempData["message"] = "La adición de imágenes de productos se ha realizado correctamente.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al agregar imágenes de productos.";
                return RedirectToAction(nameof(ProductPhotoCreate));
            }
            return RedirectToAction(nameof(ProductPhotoCreate));
        }
        public ActionResult ProductPhotoEdit(int? id)
        {
            Picture model = new Picture();
            model.ProductId = Convert.ToInt32(Session["productId"]);
            model.UserId = Convert.ToString(Session["TiendaId"]);

            var updatePhoto = _pictureService.GetAllIncluding().Where(i => i.ProductId == id).FirstOrDefault(i => i.Id == id);
            if (updatePhoto != null)
            {
                return View(updatePhoto);
            }
            return RedirectToAction(nameof(PictureList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductPhotoEdit(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.ProductId = Convert.ToInt32(Session["productId"]);
                model.UserId = Convert.ToString(Session["TiendaId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    _pictureService.Update(model);
                    TempData["message"] = "La actualización de las imágenes del producto se realizó correctamente.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "Se produjo un error al actualizar las imágenes del producto.";
                return RedirectToAction(nameof(ProductPhotoEdit));
            }
            return RedirectToAction(nameof(ProductPhotoEdit));
        }
        public ActionResult ControlProduct(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlProductDetailList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productDetailService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlProductDetail(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_productService.GetAllIncluding().Where(i => i.UserId == userId && i.ProductDetailId == id).FirstOrDefault());
        }
        public ActionResult ControlCompany(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlSocialMedia(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companySocialMediaService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlContact(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_companyContactService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlPicture(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlCompanyPicture(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ControlProductPicture(string userId, int? id, int page = 1)
        {
            userId = Convert.ToString(Session["TiendaId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult DeleteProduct(int id)
        {
            var deleteProduct = _productService.GetById(id);
            if (deleteProduct != null)
            {
                _productService.Delete(deleteProduct);
            }
            return RedirectToAction(nameof(ControlProduct));
        }
        public ActionResult DeleteProductDetail(int id)
        {
            var deleteProductDetail = _productDetailService.GetById(id);
            if (deleteProductDetail != null)
            {
                _productDetailService.Delete(deleteProductDetail);
            }
            return RedirectToAction(nameof(ControlProductDetail));
        }
        public ActionResult DeleteContact(int id)
        {
            var deleteContact = _companyContactService.GetById(id);
            if (deleteContact != null)
            {
                _companyContactService.Delete(deleteContact);
            }
            return RedirectToAction(nameof(ControlContact));
        }
        public ActionResult DeleteSocialMedia(int id)
        {
            var deleteSocialMedia = _companySocialMediaService.GetById(id);
            if (deleteSocialMedia != null)
            {
                _companySocialMediaService.Delete(deleteSocialMedia);
            }
            return RedirectToAction(nameof(ControlSocialMedia));
        }
        public ActionResult DeletePicture(int id)
        {
            var deletePicture = _pictureService.GetById(id);
            if (deletePicture != null)
            {
                _pictureService.Delete(deletePicture);
            }
            return RedirectToAction(nameof(ControlPicture));
        }
        public ActionResult DeleteCompanyPicture(int id)
        {
            var deleteCompanyPicture = _pictureService.GetById(id);
            if (deleteCompanyPicture != null)
            {
                _pictureService.Delete(deleteCompanyPicture);
            }
            return RedirectToAction(nameof(ControlCompanyPicture));
        }
        public ActionResult DeleteProductPicture(int id)
        {
            var deleteProductPicture = _pictureService.GetById(id);
            if (deleteProductPicture != null)
            {
                _pictureService.Delete(deleteProductPicture);
            }
            return RedirectToAction(nameof(ControlProductPicture));
        }
        public ActionResult ActiveCompany(int id)
        {
            _companyService.SetActive(id);
            return RedirectToAction(nameof(ControlCompany));
        }
        public ActionResult DeActiveCompany(int id)
        {
            _companyService.SetDeActive(id);
            return RedirectToAction(nameof(ControlCompany));
        }
        public ActionResult ActiveProduct(int id)
        {
            _productService.SetActive(id);
            return RedirectToAction(nameof(ControlProduct));
        }
        public ActionResult DeActiveProduct(int id)
        {
            _productService.SetDeActive(id);
            return RedirectToAction(nameof(ControlProduct));
        }
        public ActionResult ActiveProductDetail(int id)
        {
            _productDetailService.SetActive(id);
            return RedirectToAction(nameof(ControlProductDetailList));
        }
        public ActionResult DeActiveProductDetail(int id)
        {
            _productDetailService.SetDeActive(id);
            return RedirectToAction(nameof(ControlProductDetailList));
        }
        public ActionResult ActiveContact(int id)
        {
            _companyContactService.SetActive(id);
            return RedirectToAction(nameof(ControlContact));
        }
        public ActionResult DeActiveContact(int id)
        {
            _companyContactService.SetDeActive(id);
            return RedirectToAction(nameof(ControlContact));
        }
        public ActionResult ActiveSocialMedia(int id)
        {
            _companySocialMediaService.SetActive(id);
            return RedirectToAction(nameof(ControlSocialMedia));
        }
        public ActionResult DeActiveSocialMedia(int id)
        {
            _companySocialMediaService.SetDeActive(id);
            return RedirectToAction(nameof(ControlSocialMedia));
        }
        public ActionResult ActivePicture(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(ControlPicture));
        }
        public ActionResult DeActivePicture(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(ControlPicture));
        }
        public ActionResult ActiveCompanyPicture(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(ControlCompanyPicture));
        }
        public ActionResult DeActiveCompanyPicture(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(ControlCompanyPicture));
        }
        public ActionResult ActiveProductPicture(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(ControlProductPicture));
        }
        public ActionResult DeActiveProductPicture(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(ControlProductPicture));
        }
        public ActionResult ActiveOrder(int id)
        {
            _orderService.SetActive(id);
            return RedirectToAction(nameof(HomeCompanyController.OrderList));
        }
        public ActionResult DeActiveOrder(int id)
        {
            _orderService.SetDeActive(id);
            return RedirectToAction(nameof(HomeCompanyController.OrderList));
        }
        public ActionResult ActiveSent(int id)
        {
            _orderService.ActiveSent(id);
            return RedirectToAction(nameof(HomeCompanyController.OrderList));
        }
        public ActionResult DeActiveSent(int id)
        {
            _orderService.ActiveSent(id);
            return RedirectToAction(nameof(HomeCompanyController.OrderList));
        }
    }
}