using Ninject;
using Ninject.Modules;
using OrderFood.Business.Interfaces;
using OrderFood.Business.Repositories;
using OrderFood.Service.AdService;
using OrderFood.Service.ApplicationUserService;
using OrderFood.Service.CancelRequestService;
using OrderFood.Service.CategoryService;
using OrderFood.Service.CityService;
using OrderFood.Service.CommentService;
using OrderFood.Service.CompanyContactService;
using OrderFood.Service.CompanyService;
using OrderFood.Service.CompanySocialMediaService;
using OrderFood.Service.ComplainService;
using OrderFood.Service.ContactService;
using OrderFood.Service.CountryService;
using OrderFood.Service.KindService;
using OrderFood.Service.LogService;
using OrderFood.Service.MessageForUserService;
using OrderFood.Service.MessageFromCompanyService;
using OrderFood.Service.OrderService;
using OrderFood.Service.PaymentService;
using OrderFood.Service.PictureService;
using OrderFood.Service.ProductDetailService;
using OrderFood.Service.ProductService;
using OrderFood.Service.ProfilePhotoService;
using OrderFood.Service.SendMailService;
using OrderFood.Service.SliderService;
using OrderFood.Service.SocialMediaService;
using OrderFood.Service.SubcategoryService;
using OrderFood.Service.ToMakeService;
using OrderFood.Service.VideoAdService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrderFood.Web.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public NinjectControllerFactory()
        {
            kernel = new StandardKernel(new NinjectBindingModule());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)kernel.Get(controllerType);
        }
    }
    public class NinjectBindingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAdRepository>().To<AdRepository>();
            Kernel.Bind<IAdService>().To<AdService>();
            Kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>();
            Kernel.Bind<IApplicationUserService>().To<ApplicationUserService>();
            Kernel.Bind<ICancelRequestRepository>().To<CancelRequestRepository>();
            Kernel.Bind<ICancelRequestService>().To<CancelRequestService>();
            Kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            Kernel.Bind<ICategoryService>().To<CategoryService>();
            Kernel.Bind<ICityRepository>().To<CityRepository>();
            Kernel.Bind<ICityService>().To<CityService>();
            Kernel.Bind<ICommentRepository>().To<CommentRepository>();
            Kernel.Bind<ICommentService>().To<CommentService>();
            Kernel.Bind<ICompanyContactRepository>().To<CompanyContactRepository>();
            Kernel.Bind<ICompanyContactService>().To<CompanyContactService>();
            Kernel.Bind<ICompanySocialMediaRepository>().To<CompanySocialMediaRepository>();
            Kernel.Bind<ICompanySocialMediaService>().To<CompanySocialMediaService>();
            Kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            Kernel.Bind<ICompanyService>().To<CompanyService>();           
            Kernel.Bind<IComplainRepository>().To<ComplainRepository>();
            Kernel.Bind<IComplainService>().To<ComplainService>();
            Kernel.Bind<IContactRepository>().To<ContactRepository>();
            Kernel.Bind<IContactService>().To<ContactService>();
            Kernel.Bind<ICountryRepository>().To<CountryRepository>();          
            Kernel.Bind<ICountryService>().To<CountryService>();            
            Kernel.Bind<IKindRepository>().To<KindRepository>();
            Kernel.Bind<IKindService>().To<KindService>();
            Kernel.Bind<ILogRepository>().To<LogRepository>();
            Kernel.Bind<ILogService>().To<LogService>();
            Kernel.Bind<IMessageForUserRepository>().To<MessageForUserRepository>();
            Kernel.Bind<IMessageForUserService>().To<MessageForUserService>();
            Kernel.Bind<IMessageFromCompanyRepository>().To<MessageFromCompanyRepository>();
            Kernel.Bind<IMessageFromCompanyService>().To<MessageFromCompanyService>();
            Kernel.Bind<IOrderRepository>().To<OrderRepository>();
            Kernel.Bind<IOrderService>().To<OrderService>();
            Kernel.Bind<IPaymentRepository>().To<PaymentRepository>();
            Kernel.Bind<IPaymentService>().To<PaymentService>();
            Kernel.Bind<IPictureRepository>().To<PictureRepository>();
            Kernel.Bind<IPictureService>().To<PictureService>();
            Kernel.Bind<IProductDetailRepository>().To<ProductDetailRepository>();
            Kernel.Bind<IProductDetailService>().To<ProductDetailService>();
            Kernel.Bind<IProductRepository>().To<ProductRepository>();
            Kernel.Bind<IProductService>().To<ProductService>();
            Kernel.Bind<IProfilePhotoRepository>().To<ProfilePhotoRepository>();
            Kernel.Bind<IProfilePhotoService>().To<ProfilePhotoService>();
            Kernel.Bind<ISendMailRepository>().To<SendMailRepository>();
            Kernel.Bind<ISendMailService>().To<SendMailService>();
            Kernel.Bind<ISliderRepository>().To<SliderRepository>();
            Kernel.Bind<ISliderService>().To<SliderService>();
            Kernel.Bind<ISocialMediaRepository>().To<SocialMediaRepository>();
            Kernel.Bind<ISocialMediaService>().To<SocialMediaService>();
            Kernel.Bind<ISubcategoryRepository>().To<SubcategoryRepository>();
            Kernel.Bind<ISubcategoryService>().To<SubcategoryService>();
            Kernel.Bind<IToMakeRepository>().To<ToMakeRepository>();
            Kernel.Bind<IToMakeService>().To<ToMakeService>();
            Kernel.Bind<IVideoAdRepository>().To<VideoAdRepository>();
            Kernel.Bind<IVideoAdService>().To<VideoAdService>();
        }
    }
}