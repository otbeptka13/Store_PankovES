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
            return View("_404");
        }

     
    }
}