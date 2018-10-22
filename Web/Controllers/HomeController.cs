using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Link(description = "Главная", url = "~/")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Link(description = "Инфо")]
        public ActionResult Info(InfoModel model)
        {
            ModelState.Clear();
            return View(model);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}