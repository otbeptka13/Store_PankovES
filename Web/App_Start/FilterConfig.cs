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
        // перехватывает необработанные исключения
        public override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            var route = new RouteValueDictionary();
            var message = HttpUtility.UrlPathEncode(filterContext.Exception.Message);
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new JsonResult();
                result.Data = new { result = -100, message = filterContext.Exception.Message };
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = result;               
            }
            else //if (!filterContext.HttpContext.Session.IsAuth())
                filterContext.HttpContext.Response.Redirect("/Error/Custom?message=" + message, true);


        }
        
    }

}
