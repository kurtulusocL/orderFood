using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.CategoryService;
using OrderFood.Service.CityService;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.ComplainService;
using OrderFood.Service.CountryService;
using OrderFood.Service.KindService;
using OrderFood.Service.OrderService;
using OrderFood.Service.ProductService;
using OrderFood.Service.SubcategoryService;
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
    [Authorize(Roles = "Admin,Helper,Asistant")]
    public class AdminIstatisticController : Controller
    {
        IApplicationUserService _userService;
        ICompanyService _companyService;
        IProductService _productService;       
        ICityService _cityService;
        ICountryService _countryService;
        ICategoryService _categoryService;
        ISubcategoryService _subcategoryService;
        IKindService _kindService;
        public AdminIstatisticController(IApplicationUserService userService, ICompanyService companyService, IProductService productService, ICityService cityService, ICountryService countryService, ICategoryService categoryService, ISubcategoryService subcategoryService, IKindService kindService)
        {
            _userService = userService;
            _companyService = companyService;
            _productService = productService;
            _cityService = cityService;
            _countryService = countryService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _kindService = kindService;
        }
        public ActionResult CompanyList(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }
        public ActionResult ProductIstatistic(int? id)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).FirstOrDefault());
        }        
        public ActionResult CompanyIstatistic(int? id)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Id == id).FirstOrDefault());
        }
        public ActionResult GeneralProductIstatistic()
        {
            return View();
        }        
        public ActionResult GeneralCompanyIstatistic()
        {
            return View();
        }
        public ActionResult OurIstatistic()
        {
            return View();
        }
        public ActionResult UserIstatistic()
        {
            return View();
        }
        public ActionResult MostSellProduct(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult LessSellProduct(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderBy(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult MostSellCity(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult LessSellCity(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderBy(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult MostSellCountry(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult _CompanyInfo(int? id)
        {
            return PartialView(_companyService.GetAllIncluding().Where(i => i.Id == id).FirstOrDefault());
        }
        public ActionResult LessSellCountry(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderBy(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult CountryMostSell(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult CountryLessSell(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CountryId == id).OrderBy(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult CityMostSell(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 40));
        }
        public ActionResult CityLessSell(int? id, int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CityId == id).OrderBy(i => i.Orders.Count()).ToPagedList(page, 40));
        }   
        public ActionResult CompanyProduct(int? id)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Id == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult CompanyInfoIstatistic(int? id)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Id == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult CompanyPopularity(int? id)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Id == id).OrderByDescending(i => i.CreatedDate).ToList());
        }
        public ActionResult GeneralMostSellProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessSellProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostPopularProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Like).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessPopularProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Like).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostExpensiveProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.SellingPrice).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostCheapProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.SellingPrice).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostComplainProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessComplainProduct(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralProductMostStatus(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralProductLessStatus(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Complains.Count()).ToPagedList(page, 30));
        }   
        public ActionResult GeneralMostPopularCompany(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Like).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessPopularCompany(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Like).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostSellingCompany(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessSellingCompany(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostCompanyProduct(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessCompanyProduct(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Products.Count()).ToPagedList(page, 30));
        }        
        public ActionResult GeneralMostInteractionCompany(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Hit).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessInteractionCompany(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Hit).ToPagedList(page, 30));
        }
        public ActionResult GeneralMostCompanyStatus(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult GeneralLessCompanyStatus(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostProductCity(int page = 1)
        {
            return View(_cityService.GetAllIncluding().OrderByDescending(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessProductCity(int page = 1)
        {
            return View(_cityService.GetAllIncluding().OrderByDescending(i => i.IsConfirmed == true).OrderBy(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostProductCountry(int page = 1)
        {
            return View(_countryService.GetAllIncluding().OrderByDescending(i => i.IsConfirmed == true).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessProductCountry(int page = 1)
        {
            return View(_countryService.GetAllIncluding().OrderByDescending(i => i.IsConfirmed == true).OrderBy(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostOrderCity(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessOrderCity(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostOrderCompanyKind(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessOrderCompanyKind(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostOrderProductCategory(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessOrderProductCategory(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Orders.Count()).ToPagedList(page, 30));
        }        
        public ActionResult MostCategoryCompany(int page = 1)
        {
            return View(_kindService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Companies.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessCategoryCompany(int page = 1)
        {
            return View(_kindService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Companies.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostCategoryProduct(int page = 1)
        {
            return View(_categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessCategoryProduct(int page = 1)
        {
            return View(_categoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() >= 0).OrderBy(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostSubcategoryProduct(int page = 1)
        {
            return View(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() > 0).OrderByDescending(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessSubcategoryProduct(int page = 1)
        {
            return View(_subcategoryService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.Products.Count() >= 0).OrderBy(i => i.Products.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostCompanyComment(int page=1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Comments.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessCompanyComment(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Comments.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostProductComment(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Comments.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessProductComment(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Comments.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostCompanyComplain(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessCompanyComplain(int page = 1)
        {
            return View(_companyService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult MostProductComplain(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.Complains.Count()).ToPagedList(page, 30));
        }
        public ActionResult LessProductComplain(int page = 1)
        {
            return View(_productService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderBy(i => i.Complains.Count()).ToPagedList(page, 30));
        }
    }
}