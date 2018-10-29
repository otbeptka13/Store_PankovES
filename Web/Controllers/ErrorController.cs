using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult _404()
        {
            var str = "Что-то пошло не так...";
            return View("_404", (object)str);
        }
        public ActionResult Index(string message)
        {
            return View("_404", (object)message);
        }
        public ActionResult Custom(string message)
        {
            return View("Custom",(object)message);
        }
    }
}