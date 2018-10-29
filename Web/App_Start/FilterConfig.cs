using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);
            var route = new RouteValueDictionary();
            var message = HttpUtility.UrlPathEncode(filterContext.Exception.Message);
            filterContext.HttpContext.Response.Redirect("/Error/Custom?message=" + message, true);
        }
    }

}
