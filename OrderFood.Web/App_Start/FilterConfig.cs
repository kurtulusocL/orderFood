using OrderFood.Web.Models;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeFilterAttribute());
        }
    }
}
