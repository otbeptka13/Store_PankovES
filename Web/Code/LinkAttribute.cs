using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web
{
    internal class LinkAttribute : ActionFilterAttribute, IActionFilter
    {
        public string url;
        public string description;
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Add your action filter's tasks here
            filterContext.Controller.ViewBag.Links = new Queue<Link>();
            var attributesController = filterContext.Controller.GetType().GetCustomAttributes(typeof(LinkAttribute), true);
            var attributesAction = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LinkAttribute), true);
            if (attributesController.Length > 0)
            {
                var attribute = attributesController[0] as LinkAttribute;
                (filterContext.Controller.ViewBag.Links as Queue<Link>).Enqueue(new Link { description = attribute.description, url = attribute.url });
            }
            if (attributesAction.Length > 0)
            {
                var attribute = attributesAction[0] as LinkAttribute;
                (filterContext.Controller.ViewBag.Links as Queue<Link>).Enqueue(new Link { description = attribute.description, url = attribute.url });
            }       
            base.OnActionExecuting(filterContext);
        }
    }
    public class Link
    {
        public string url;
        public string description;
    }
}