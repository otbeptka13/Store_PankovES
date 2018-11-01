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

        public JsonResult AddBasket(long goodId, int count)
        {
            var customer = new CustomerAction(Session.GetUser().id);
            try
            {
                var addBasketModel = new AddBasketModel
                {
                    goodId = goodId,
                    count = count,
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
                Session.UpdateWishList();
                var wishList = Session.GetUser().wishList;
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
        public JsonResult DeleteWishList(long goodId)
        {
            var resultCode = 0;
            var customer = new CustomerAction(Session.GetUserId());
            try
            {
                customer.DeleteWishList(goodId);
            }
            catch (Exception)
            {
                resultCode = -1;
            }
            try
            {
                Session.UpdateWishList();
                var wishList = Session.GetUser().wishList;
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
        [Link(description = "Корзина")]
        public ActionResult Basket()
        {
            var customer = new CustomerAction(Session.GetUserId());
            var basket = customer.GetBasket();
            return View(basket);
        }
        public ActionResult PayBasket(string idcount, bool? isFast)
        {
            var customer = new CustomerAction(Session.GetUserId());
            Session.UpdateBasket();
            var basket = Session.GetUser().basket;
            if (isFast != true)
            {
                if (string.IsNullOrWhiteSpace(idcount))
                    return View("Basket");
                var splitBasket = idcount.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (basket.Count != splitBasket.Count())
                {
                    var message = "Количество товара в корзине не соответствует ожидаемому";
                    return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
                }
                foreach (var item in splitBasket)
                {
                    var bask = item.Trim().Split(':');
                    var basketId = Int64.Parse(bask[0]);
                    var count = decimal.Parse(bask[1]);
                    if (!basket.Any(s => s.id == basketId))
                    {
                        var message = "Идентификатор корзины был испорчен!!";
                        return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
                    }
                    var basketElement = basket.Single(s => s.id == basketId);
                    if (basketElement.count != count && count > 0)
                    {
                        customer.UpdateCount(basketElement.id, count);
                    }
                }
                Session.UpdateBasket();
                basket = Session.GetUser().basket;
            }
            var payModel = new PayModel
            {
                countInBasket = basket.Count(),
                payDate = DateTime.Now,
                totalSumm = basket.Sum(s => s.summTotal) ?? 0.00M,
                transactionNumber = Guid.NewGuid().ToString(),
                packId = basket.Select(s => s.packId).Distinct().Single() ?? 1
            };
            customer.Pay(payModel);
            Session.UpdateBasket();
            var mess = $"Заказ на сумму {payModel.totalSumm} руб. успешно создан. Идентификатор заказа: {payModel.transactionNumber}";
            return RedirectToAction("Info", "Home", new InfoModel { message = mess, header = "Оплачено!", status = StatusMessage.Accept });
        }
        [Link(description = "Избранное")]
        public ActionResult WishList()
        {
            var customer = new CustomerAction(Session.GetUserId());
            var wishlist = customer.GetWishList();
            return View(wishlist);
        }
    }


}
