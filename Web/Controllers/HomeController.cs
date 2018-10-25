using StoreDomainObject;
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
        public PartialViewResult RecentlyViewed()
        {
            var store = new StoreAction();
            var goods = store.GetNowWatching().Select(s => new GoodViewModel(s)).ToList();
            foreach (var item in goods.Where(s => SaleNewPopular.populars.Any(w => w.id == s.id)))
            {
                item.isBestseller = true;
            }
            foreach (var item in goods.Where(s => SaleNewPopular.newes.Any(w => w.id == s.id)))
            {
                item.isNew = true;
            }

            return PartialView("_recentlyViewed", goods);
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