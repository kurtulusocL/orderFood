using OrderFood.Entity.Models;
using OrderFood.Service.CategoryService;
using OrderFood.Service.CityService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Service.CountryService;
using OrderFood.Service.KindService;
using OrderFood.Service.PictureService;
using OrderFood.Service.ProductDetailService;
using OrderFood.Service.ProductService;
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
    public class AdminCreateController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyContactService _companyContactService;
        private readonly ICompanySocialMediaService _companySocialMediaService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IKindService _kindService;
        private readonly IProductDetailService _productDetailService;
        public AdminCreateController(ICompanyService companyService, ICompanyContactService companyContactService, ICompanySocialMediaService companySocialMediaService, IProductService productService, IPictureService pictureService, ICategoryService categoryService, ISubcategoryService subcategoryService, ICountryService countryService, ICityService cityService, IKindService kindService, IProductDetailService productDetailService)
        {
            _companyService = companyService;
            _companyContactService = companyContactService;
            _companySocialMediaService = companySocialMediaService;
            _productService = productService;
            _pictureService = pictureService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _countryService = countryService;
            _cityService = cityService;
            _kindService = kindService;
            _productDetailService = productDetailService;
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_companyService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ProductList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_productService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyContactList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_companyContactService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanySocialMediaList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_companySocialMediaService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult PictureList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 60));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CompanyPictureList(int? id, string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ProductPictureList(int? id, string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_pictureService.GetAllIncluding().Where(i => i.UserId == userId && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult ProductDetailList(string userId, int page = 1)
        {
            userId = Convert.ToString(Session["adminId"]);
            return View(_productDetailService.GetAllIncluding().Where(i => i.UserId == userId).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        public JsonResult CountryCity(int? countryId, int? categoryId, string tip)
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
                    Text = "Something went wrong",
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
                    Text = "Something went wrong",
                    Value = "Default"
                });
            }
            return Json(new { ok = basariliMi, text = sonuc });
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult CompanyCreate()
        {
            ViewBag.Kinds = _kindService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Companies.Count()).ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCreate(Company model, HttpPostedFileBase image)
        {
            try
            {
                model.UserId = Convert.ToString(Session["adminId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/logo/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companyService.Create(model);
                TempData["message"] = "Adding company is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding company";
                return RedirectToAction(nameof(CompanyCreate));
            }
            return RedirectToAction(nameof(CompanyCreate));
        }

        [Authorize(Roles = "Admin,Asistant")]
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

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyEdit(Company model, HttpPostedFileBase image)
        {
            try
            {
                model.UserId = Convert.ToString(Session["adminId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/logo/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companyService.Update(model);
                TempData["message"] = "Firm update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the company.";
                return RedirectToAction(nameof(CompanyEdit));
            }
            return RedirectToAction(nameof(CompanyEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult SocialMediaCreate(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SocialMediaCreate(CompanySocialMedia model, HttpPostedFileBase image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["adminId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/companysocial/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companySocialMediaService.Create(model);
                TempData["message"] = "Adding a company social media account is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the company social media account.";
                return RedirectToAction(nameof(SocialMediaCreate));
            }
            return RedirectToAction("SocialMediaCreate");
        }

        [Authorize(Roles = "Admin,Asistant")]
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

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SocialMediaEdit(CompanySocialMedia model, HttpPostedFileBase image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["adminId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/companysocial/" + image.FileName));
                    model.Logo = image.FileName;
                }
                _companySocialMediaService.Update(model);
                TempData["message"] = "Company social media account update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the company social media account.";
                return RedirectToAction(nameof(SocialMediaEdit));
            }
            return RedirectToAction(nameof(SocialMediaEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ContactCreate(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactCreate(CompanyContact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["adminId"]);
                    _companyContactService.Create(model);
                    TempData["message"] = "Adding company contact information is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding company contact information.";
                return RedirectToAction(nameof(ContactCreate));
            }
            return RedirectToAction("ContactCreate");
        }

        [Authorize(Roles = "Admin,Asistant")]
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

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactEdit(CompanyContact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["adminId"]);
                    _companyContactService.Update(model);
                    TempData["message"] = "Updating company contact information is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating company contact information.";
                return RedirectToAction(nameof(ContactEdit));
            }
            return RedirectToAction(nameof(ContactEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ProductCreate(int? id)
        {
            Session["companyId"] = id;
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["adminId"]);
                    model.ProductDetail.UserId = Convert.ToString(Session["adminId"]);
                    _productService.Create(model);
                    TempData["message"] = "Adding product is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding the product.";
                return RedirectToAction(nameof(ProductCreate));
            }
            return RedirectToAction(nameof(ProductCreate));
        }

        [Authorize(Roles = "Admin,Asistant")]
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

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    model.UserId = Convert.ToString(Session["adminId"]);
                    model.ProductDetail.UserId = Convert.ToString(Session["adminId"]);
                    _productService.Update(model);
                    TempData["message"] = "The product update process is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the product.";
                return RedirectToAction(nameof(ProductEdit));
            }
            return RedirectToAction(nameof(ProductEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
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

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductPriceEdit(ProductEditModel model)
        {
            try
            {
                var product = _productService.GetById(model.Id);
                product.SellingPrice = model.Price;
                _productService.Update(product);
                TempData["message"] = "The product price update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating the product price.";
                return RedirectToAction(nameof(ProductPriceEdit));
            }
            return RedirectToAction(nameof(ProductPriceEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
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

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDiscountEdit(ProductEditDiscount model)
        {
            try
            {
                var product = _productService.GetById(model.Id);
                product.DicsountPrice = model.Discount;
                _productService.Update(product);
                TempData["message"] = "The product discount price update process is successful.";
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while the product was updating the sale price.";
                return RedirectToAction(nameof(ProductDiscountEdit));
            }
            return RedirectToAction(nameof(ProductDiscountEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult CompanyPhotoCreate(int? id)
        {
            Session["companyId"] = id;
            var photo = _pictureService.GetAllIncluding().FirstOrDefault(i => i.CompanyId == id);
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyPhotoCreate(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["adminId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    _pictureService.Create(model);
                    TempData["message"] = "Adding company pictures is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding company pictures.";
                return RedirectToAction(nameof(CompanyPhotoCreate));
            }
            return RedirectToAction(nameof(CompanyPhotoCreate));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult CompanyPhotoEdit(int? id)
        {
            Picture model = new Picture();
            model.CompanyId = Convert.ToInt32(Session["companyId"]);
            model.UserId = Convert.ToString(Session["adminId"]);

            var updatePhoto = _pictureService.GetAllIncluding().Where(i => i.CompanyId == id).FirstOrDefault(i => i.Id == id);
            if (updatePhoto != null)
            {
                return View(updatePhoto);
            }
            return RedirectToAction(nameof(PictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyPhotoEdit(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.CompanyId = Convert.ToInt32(Session["companyId"]);
                model.UserId = Convert.ToString(Session["adminId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    model.CompanyId = Convert.ToInt32(Session["companyId"]);
                    _pictureService.Update(model);
                    TempData["message"] = "Updating company pictures is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating company pictures.";
                return RedirectToAction(nameof(CompanyPhotoEdit));
            }
            return RedirectToAction(nameof(CompanyPhotoEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ProductPhotoCreate(int? id)
        {
            Session["productId"] = id;
            var photo = _pictureService.GetAllIncluding().FirstOrDefault(i => i.ProductId == id);
            return View();
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductPhotoCreate(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.ProductId = Convert.ToInt32(Session["productId"]);
                model.UserId = Convert.ToString(Session["adminId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    model.ProductId = Convert.ToInt32(Session["productId"]);
                    _pictureService.Create(model);
                    TempData["message"] = "Adding product images is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while adding product pictures";
                return RedirectToAction(nameof(ProductPhotoCreate));
            }
            return RedirectToAction(nameof(ProductPhotoCreate));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ProductPhotoEdit(int? id)
        {
            Picture model = new Picture();
            model.ProductId = Convert.ToInt32(Session["productId"]);
            model.UserId = Convert.ToString(Session["adminId"]);

            var updatePhoto = _pictureService.GetAllIncluding().Where(i => i.ProductId == id).FirstOrDefault(i => i.Id == id);
            if (updatePhoto != null)
            {
                return View(updatePhoto);
            }
            return RedirectToAction(nameof(PictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductPhotoEdit(Picture model, IEnumerable<HttpPostedFileBase> image)
        {
            try
            {
                model.ProductId = Convert.ToInt32(Session["productId"]);
                model.UserId = Convert.ToString(Session["adminId"]);

                foreach (var item in image)
                {
                    model.Title = Path.GetFileName(item.FileName);
                    model.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                    item.SaveAs(model.ImageUrl);
                    model.ImageUrl = model.Title;

                    model.ProductId = Convert.ToInt32(Session["productId"]);
                    _pictureService.Update(model);
                    TempData["message"] = "Updating product images is successful.";
                }
            }
            catch (Exception)
            {
                TempData["err"] = "An error occurred while updating product pictures.";
                return RedirectToAction(nameof(ProductPhotoEdit));
            }
            return RedirectToAction(nameof(ProductPhotoEdit));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveCompany(int id)
        {
            _companyService.SetActive(id);
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveCompany(int id)
        {
            _companyService.SetDeActive(id);
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveProduct(int id)
        {
            _productService.SetActive(id);
            return RedirectToAction(nameof(ProductList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveProduct(int id)
        {
            _productService.SetDeActive(id);
            return RedirectToAction(nameof(ProductList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveProductDetail(int id)
        {
            _productDetailService.SetActive(id);
            return RedirectToAction(nameof(ProductDetail));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveProductDetail(int id)
        {
            _productDetailService.SetDeActive(id);
            return RedirectToAction(nameof(ProductDetail));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveContact(int id)
        {
            _companyContactService.SetActive(id);
            return RedirectToAction(nameof(CompanyContactList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveContact(int id)
        {
            _companyContactService.SetDeActive(id);
            return RedirectToAction(nameof(CompanyContactList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveSocialMedia(int id)
        {
            _companySocialMediaService.SetActive(id);
            return RedirectToAction(nameof(CompanySocialMediaList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveSocialMedia(int id)
        {
            _companySocialMediaService.SetDeActive(id);
            return RedirectToAction(nameof(CompanySocialMediaList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActivePicture(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(PictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActivePicture(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(PictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveCompanyPicture(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(CompanyPictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveCompanyPicture(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(CompanyPictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult ActiveProductPicture(int id)
        {
            _pictureService.SetActive(id);
            return RedirectToAction(nameof(ProductPictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActiveProductPicture(int id)
        {
            _pictureService.SetDeActive(id);
            return RedirectToAction(nameof(ProductPictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteCompany(int id)
        {
            var deleteCompany = _companyService.GetById(id);
            if (deleteCompany != null)
            {
                _companyService.Delete(deleteCompany);
            }
            return RedirectToAction(nameof(CompanyList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteProduct(int id)
        {
            var deleteProduct = _productService.GetById(id);
            if (deleteProduct != null)
            {
                _productService.Delete(deleteProduct);
            }
            return RedirectToAction(nameof(ProductList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteProductDetail(int id)
        {
            var deleteProductDetail = _productDetailService.GetById(id);
            if (deleteProductDetail != null)
            {
                _productDetailService.Delete(deleteProductDetail);
            }
            return RedirectToAction(nameof(ProductDetail));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteContact(int id)
        {
            var deleteContact = _companyContactService.GetById(id);
            if (deleteContact != null)
            {
                _companyContactService.Delete(deleteContact);
            }
            return RedirectToAction(nameof(CompanyContactList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteSocialMedia(int id)
        {
            var deleteSocialMedia = _companySocialMediaService.GetById(id);
            if (deleteSocialMedia != null)
            {
                _companySocialMediaService.Delete(deleteSocialMedia);
            }
            return RedirectToAction(nameof(CompanySocialMediaList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeletePicture(int id)
        {
            var deletePicture = _pictureService.GetById(id);
            if (deletePicture != null)
            {
                _pictureService.Delete(deletePicture);
            }
            return RedirectToAction(nameof(PictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteCompanyPicture(int id)
        {
            var deleteCompanyPicture = _pictureService.GetById(id);
            if (deleteCompanyPicture != null)
            {
                _pictureService.Delete(deleteCompanyPicture);
            }
            return RedirectToAction(nameof(CompanyPictureList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeleteProductPicture(int id)
        {
            var deleteProductPicture = _pictureService.GetById(id);
            if (deleteProductPicture != null)
            {
                _pictureService.Delete(deleteProductPicture);
            }
            return RedirectToAction(nameof(ProductPictureList));
        }        
    }
}