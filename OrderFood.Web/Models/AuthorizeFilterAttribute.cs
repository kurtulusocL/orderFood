using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrderFood.Web.Models
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.IsInRole("Admin") || !filterContext.HttpContext.User.IsInRole("Asistant"))
            {               
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } }); 
            }
            if (!filterContext.HttpContext.User.IsInRole("Admin"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }
            if (!filterContext.HttpContext.User.IsInRole("Admin") || !filterContext.HttpContext.User.IsInRole("Asistant")|| !filterContext.HttpContext.User.IsInRole("Helper"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }
            if (!filterContext.HttpContext.User.IsInRole("Tienda"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }
            if (!filterContext.HttpContext.User.IsInRole("Users"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }
            if (filterContext.HttpContext.Session["userId"] == null || string.IsNullOrEmpty(filterContext.HttpContext.Session["userId"].ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }
            if (filterContext.HttpContext.Session["adminId"] == null || string.IsNullOrEmpty(filterContext.HttpContext.Session["adminId"].ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }           
            if (filterContext.HttpContext.Session["TiendaId"] == null || string.IsNullOrEmpty(filterContext.HttpContext.Session["TiendaId"].ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Page401" } });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}