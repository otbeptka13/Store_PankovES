using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
 
    public class CustomerController : Controller
    {
        public JsonResult DeleteBasket(long basketId)
        {
            var customer = new CustomerAction(Session.GetUser().id);
            try
            {
                customer.DelBasket(basketId);
                Session.UpdateBasket();
                var basketSum = customer.GetBasket()?.Sum(s => s.summTotal) ?? 0.00M;
                var basketCount = customer.GetBasket()?.Count() ?? 0;
                return Json(new { result = 0, basketSum = basketSum, basketCount = basketCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = -1, message = "Ошибка удаления из корзины. Перезагрузите страницу" }, JsonRequestBehavior.AllowGet);
            }
        }
    }


}
