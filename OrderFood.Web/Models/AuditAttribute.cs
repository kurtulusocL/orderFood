using OrderFood.Data.Data;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderFood.Web.Models
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;           
            Audit audit = new Audit()
            {
                Id = Guid.NewGuid(),
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                IPAddress = UserIPAddress.FindUserIp(),
                Browser = request.Browser.Browser,
                BrowserVersion = request.Browser.Version,
                Language = request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].Substring(0, 2),
                AreaAccessed = request.Url.LocalPath,
                Device = request.Browser.MobileDeviceManufacturer,
                IsMobile = request.Browser.IsMobileDevice,
                DeviceModel = request.Browser.MobileDeviceModel,
                Platform = request.Browser.Platform,
                MacAddress = PCMacAddress.GetMACAddress(),
                Timestamp = DateTime.Now
            };

            ApplicationDbContext context = new ApplicationDbContext();
            context.Audits.Add(audit);
            context.SaveChanges();
            base.OnActionExecuting(filterContext);
        }
    }
}