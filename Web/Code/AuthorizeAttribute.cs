using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute//FilterAttribute, IAuthorizationFilter
    {
        private Role role;
        public AuthorizeAttribute(Role role) : base()
        {
            this.role = role;
        }
        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var roleUser = httpContext.Session.GetUserRoles();
            if ((role & roleUser) == role)
                return true;
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new JsonResult();
                result.Data = new { result = -13 };
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = result;
            }
            else if (!filterContext.HttpContext.Session.IsAuth())
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
                
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
    [Flags]
    public enum Role
    {
        None, Customer, Admin
    }

}