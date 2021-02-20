using Newtonsoft.Json;
using OrderFood.Service.AdService;
using OrderFood.Service.CategoryService;
using OrderFood.Service.CityService;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Service.ContactService;
using OrderFood.Service.CountryService;
using OrderFood.Service.KindService;
using OrderFood.Service.OrderService;
using OrderFood.Service.PaymentService;
using OrderFood.Service.PictureService;
using OrderFood.Service.ProductDetailService;
using OrderFood.Service.ProductService;
using OrderFood.Service.SliderService;
using OrderFood.Service.SocialMediaService;
using OrderFood.Service.SubcategoryService;
using OrderFood.Service.VideoAdService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace OrderFood.Web.Controllers
{
    [Audit]
    public class HomeController : Controller
    {
        public class CaptchaResult
        {
            public class CaptchaResponse
            {
                [JsonProperty("success")]
                public bool Success { get; set; }

                [JsonProperty("error-codes")]
                public List<string> ErrorCodes { get; set; }
            }
        }
        private readonly IPaymentService _paymentService;
        private readonly IContactService _contactService;
        private readonly ISocialMediaService _socialMediaService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IKindService _kindService;
        private readonly ICompanyService _companyService;
        private readonly IProductService _productService;
        private readonly IAdService _adService;
        private readonly IVideoAdService _videoAdService;
        private readonly IPictureService _pictureService;
        private readonly ICompanyContactService _companyContactService;
        private readonly ICompanySocialMediaService _companySocialMediaService;
        private readonly ISliderService _sliderService;
        private readonly ICommentService _commentService;
        private readonly IProductDetailService _productDetailService;
        private readonly IOrderService _orderService;
        public HomeController(IPaymentService paymentService, IContactService contactService, ISocialMediaService socialMediaService, ICountryService countryService, ICityService cityService, ICategoryService categoryService, ISubcategoryService subcategoryService, IKindService kindService, ICompanyService companyService, IProductService productService, IAdService adService, IVideoAdService videoAdService, IPictureService pictureService, ICompanyContactService companyContactService, ICompanySocialMediaService companySocialMediaService, ISliderService sliderService, ICommentService commentService, IProductDetailService productDetailService, IOrderService orderService)
        {
            _paymentService = paymentService;
            _contactService = contactService;
            _socialMediaService = socialMediaService;
            _countryService = countryService;
            _cityService = cityService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _kindService = kindService;
            _companyService = companyService;
            _productService = productService;
            _adService = adService;
            _videoAdService = videoAdService;
            _pictureService = pictureService;
            _companyContactService = companyContactService;
            _companySocialMediaService = companySocialMediaService;
            _sliderService = sliderService;
            _commentService = commentService;
            _productDetailService = productDetailService;
            _orderService = orderService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SecurityAndCookie()
        {
            return View();
        }
        public ActionResult KVKH()
        {
            return View();
        }
        public ActionResult UserArgeement()
        {
            return View();
        }
        public ActionResult CompanyDeal()
        {
            return View();
        }
        public ActionResult _Navbar()
        {
            return PartialView();
        }
        public ActionResult _NavbarProductCategory()
        {
            return PartialView(_categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _NavbarProductSubcategory(int? id)
        {
            return PartialView(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0 && i.CategoryId == id).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _NavbarProductCountry()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _NavbarProductCity(int? id)
        {
            return PartialView(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0 && i.CountryId == id).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _NavbarCountry()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Companies.Count() > 0 || i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _NavbarCity()
        {
            return PartialView(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Companies.Count() > 0 || i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _HeaderNavbar()
        {
            return PartialView();
        }
        public ActionResult _HeaderNavbarContact()
        {
            return PartialView(_contactService.GetAll().Where(i => i.IsConfirmed == true).OrderBy(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _HeaderNavbarCountryList()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _HeaderNavbarCityList()
        {
            return PartialView(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _HeaderBox()
        {
            return PartialView();
        }
        public ActionResult _Footer()
        {
            return PartialView();
        }
        public ActionResult _LayoutPayment()
        {
            return PartialView(_paymentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToList());
        }
        public ActionResult _FooterContact()
        {
            return PartialView(_contactService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _FooterSocialMedia()
        {
            return PartialView(_socialMediaService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _CategoryCity()
        {
            return PartialView(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _CategoryCountry()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _CategoryAd()
        {
            return PartialView(_adService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => Guid.NewGuid()).Take(5).ToList());
        }
        public ActionResult _CompanyVideoAd()
        {
            return PartialView(_videoAdService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _ContactCompanyDetail(int? id)
        {
            return PartialView(_companyContactService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _SocialMediaCompanyDetail(int? id)
        {
            return PartialView(_companySocialMediaService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _ProductCompanyDetail(int? id)
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => Guid.NewGuid()).Take(16).ToList());
        }
        public ActionResult _ProductPhotoCompanyDetail(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _ProductPhotoModalCompanyDetail(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _CompanyDetailBigPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _CompanyDetailSubPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _ProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _ProductModalBigPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _ProductModalSubPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).ToList());
        }
        public ActionResult _ProductCountry()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _ProductCity()
        {
            return PartialView(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _ProductCategory()
        {
            return PartialView(_categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _ProductSubcategory()
        {
            return PartialView(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _ProductDetailBigPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _ProductDetailSubPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _ProductDetail(int? id)
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductDetailId == id).FirstOrDefault());
        }
        public ActionResult _RandomProduct()
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => Guid.NewGuid()).Take(20).ToList());
        }
        public ActionResult _RandomProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _RandomProductModalBigPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _RandomProductModalSubPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult _Slider()
        {
            return PartialView(_sliderService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => Guid.NewGuid()).ToList());
        }
        public ActionResult _HomeCountryList()
        {
            return PartialView(_countryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _HomeCityList()
        {
            return PartialView(_cityService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToList());
        }
        public ActionResult _HomeVideAd()
        {
            return PartialView(_videoAdService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _HomeCommentList()
        {
            return PartialView(_commentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).Take(20).ToList());
        }
        public ActionResult _HomeCompanySlider()
        {
            return PartialView(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count() > 0).Take(50).ToList());
        }
        public ActionResult _OrderPay()
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
        public ActionResult _MiniSepet()
        {
            if (HttpContext.Session["AktifSepet"] != null)
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            else
                return PartialView();
        }
        public ActionResult _OrderPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).Take(1).ToList());
        }
        public ActionResult _SearchPartial()
        {
            return PartialView();
        }
        public ActionResult Search(string key, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.Name.Contains(key) || i.Company.Name.Contains(key) || i.Title.Contains(key) || i.Desc.Contains(key) || i.City.Name.Contains(key) || i.Country.Name.Contains(key)).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult _CountryDetailProduct(int? id)
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.Orders.Count()).ToList());
        }
        public ActionResult _CountryDetailProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _CityDetailProduct(int? id)
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.Orders.Count()).ToList());
        }
        public ActionResult _CityDetailProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _CategoryDetailProduct(int? id)
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CategoryId == id).OrderByDescending(i => i.Orders.Count()).ToList());
        }
        public ActionResult _CategoryDetailProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _SubcategoryDetailProduct(int? id)
        {
            return PartialView(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.SubcategoryId == id).OrderByDescending(i => i.Orders.Count()).ToList());
        }
        public ActionResult _SubcategoryDetailProductPhoto(int? id)
        {
            return PartialView(_pictureService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => Guid.NewGuid()).Take(1).ToList());
        }
        public ActionResult _PromotionAd()
        {
            return PartialView(_adService.GetAll().Where(i => i.IsConfirmed == true).OrderByDescending(i => Guid.NewGuid()).Take(4).ToList());
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter xr = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xr.WriteStartDocument();
            xr.WriteStartElement("urlset");
            xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");

            xr.WriteStartElement("url");
            xr.WriteElementString("loc", "http://localhost:44306/");
            xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            xr.WriteElementString("changefreq", "daily");
            xr.WriteElementString("priority", "1");
            xr.WriteEndElement();

            var p = _adService.GetAll();
            foreach (var b in p)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Ad/" + b.Title);
                xr.WriteElementString("loc", "http://localhost:44306/Ad/" + b.Website);
                xr.WriteElementString("loc", "http://localhost:44306/Ad/" + b.CompanyName);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var zl = _videoAdService.GetAll();
            foreach (var b in zl)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/VideoAd/" + b.Title);
                xr.WriteElementString("loc", "http://localhost:44306/VideoAd/" + b.Website);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var z = _categoryService.GetAllIncluding();
            foreach (var b in z)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Category/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var zu = _subcategoryService.GetAllIncluding();
            foreach (var b in zu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Subcategory/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var zux = _countryService.GetAllIncluding();
            foreach (var b in zux)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Country/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var zuxq = _cityService.GetAllIncluding();
            foreach (var b in zuxq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/City/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var zuq = _commentService.GetAllIncluding();
            foreach (var b in zuq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Comment/" + b.Subject);
                xr.WriteElementString("loc", "http://localhost:44306/Comment/" + b.Text);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var qux = _kindService.GetAllIncluding();
            foreach (var b in qux)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Kind/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var ku = _companyService.GetAllIncluding();
            foreach (var b in ku)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Company/" + b.Name);
                xr.WriteElementString("loc", "http://localhost:44306/Company/" + b.Title);
                xr.WriteElementString("loc", "http://localhost:44306/Company/" + b.Detail);
                xr.WriteElementString("loc", "http://localhost:44306/Company/" + b.Hit.Value.ToString());
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var ka = _companyContactService.GetAllIncluding();
            foreach (var b in ka)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/CompanyContact/" + b.EMail);
                xr.WriteElementString("loc", "http://localhost:44306/CompanyContact/" + b.Address);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var kq = _companySocialMediaService.GetAllIncluding();
            foreach (var b in kq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/CompanySocialMedia/" + b.Name);
                xr.WriteElementString("loc", "http://localhost:44306/CompanySocialMedia/" + b.Url);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var kaq = _productService.GetAllIncluding();
            foreach (var b in kaq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Product/" + b.ProductNo);
                xr.WriteElementString("loc", "http://localhost:44306/Product/" + b.Name);
                xr.WriteElementString("loc", "http://localhost:44306/Product/" + b.SellingPrice);
                xr.WriteElementString("loc", "http://localhost:44306/Product/" + b.Title);
                xr.WriteElementString("loc", "http://localhost:44306/Product/" + b.Desc);
                xr.WriteElementString("loc", "http://localhost:44306/Product/" + b.Hit.Value.ToString());
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var kxq = _productDetailService.GetAllIncluding();
            foreach (var b in kxq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/ProductDetail/" + b.Warning);
                xr.WriteElementString("loc", "http://localhost:44306/ProductDetail/" + b.Offers);
                xr.WriteElementString("loc", "http://localhost:44306/ProductDetail/" + b.Articles);
                xr.WriteElementString("loc", "http://localhost:44306/ProductDetail/" + b.Detail);
                xr.WriteElementString("loc", "http://localhost:44306/ProductDetail/" + b.ShopServiceTime);
                xr.WriteElementString("loc", "http://localhost:44306/ProductDetail/" + b.OrderTime);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var kqa = _pictureService.GetAllIncluding();
            foreach (var b in kqa)
            {
                xr.WriteStartElement("url");
                if (b.ProductId!=null)
                {
                    xr.WriteElementString("loc", "http://localhost:44306/Picture/" + b.Product.Title);
                }
                else
                {
                    xr.WriteElementString("loc", "http://localhost:44306/Picture/" + b.Company.Title);
                }
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var va = _socialMediaService.GetAll();
            foreach (var b in va)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/SocialMedia/" + b.Name);
                xr.WriteElementString("loc", "http://localhost:44306/SocialMedia/" + b.Url);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var xa = _paymentService.GetAllIncluding();
            foreach (var b in xa)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Payment/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            var a = _contactService.GetAll();
            foreach (var b in a)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:44306/Contact/" + b.Address);
                xr.WriteElementString("loc", "http://localhost:44306/Contact/" + b.Location);
                xr.WriteElementString("loc", "http://localhost:44306/Contact/" + b.EMail);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            xr.WriteEndDocument();
            xr.Flush();
            xr.Close();
            Response.End();
            return View();
        }
    }
}