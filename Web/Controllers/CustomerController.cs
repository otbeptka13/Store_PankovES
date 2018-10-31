using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(role: Role.Customer)]
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

        public JsonResult AddBasket(long goodId)
        {
            var customer = new CustomerAction(Session.GetUser().id);
            try
            {
                var addBasketModel = new AddBasketModel
                {
                    goodId = goodId,
                    count = 1,
                    isFastPay = false
                };
                var result = customer.AddBasket(addBasketModel);
                Session.UpdateBasket();
                var basketSum = customer.GetBasket()?.Sum(s => s.summTotal) ?? 0.00M;
                var basketCount = customer.GetBasket()?.Count() ?? 0;
                return Json(new { result = 0, basketSum = basketSum, basketCount = basketCount,
                   basket = Session.GetUser().basket.Select(s=> new {name = s.name, summTotal = s.summTotal, id = s.id, imageUrl = s.imageUrl })
                    
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = -1, message = "Ошибка добавления в корзину" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult SendFeedback(SendFeedbackViewModel model)
        {
            var url = Request.UrlReferrer.AbsoluteUri;
            var store = new StoreAction();
            var canSendFeedback = Session.IsAuth() && !store.GetFeedBack(model.goodId).Any(s => s.userId == Session.GetUserId());
            if (canSendFeedback)
            {
                var customer = new CustomerAction(Session.GetUserId());
                customer.LeaveFeadback(new FeedBack
                {
                    date = DateTime.Now,
                    goodId = model.goodId,
                    mark = model.score,
                    message = model.message
                });
            }
            return RedirectPermanent(url);
        }

        public JsonResult AddWishList(long goodId)
        {
            var resultCode = 0;
            var customer = new CustomerAction(Session.GetUserId());
            try
            {

                customer.AddToWishList(goodId);
            }
            catch (Exception)
            {
                resultCode = -1;
            }
            try
            {
                var wishList = customer.GetWishList();
                var result = new { result = resultCode, wishCount = (customer.GetWishList()?.Count() ?? 0) };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                resultCode = -2;
                var result = new { result = resultCode, wishCount = 0 };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Basket()
        {
            return null;
        }
        public ActionResult PayBasket()
        {
            return null;
        }
        public ActionResult WishList()
        {
            return null;
        }
    }


}
