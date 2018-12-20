using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;


namespace Web.Controllers
{
    [Link(description = "Главная", url = "~/")]
    [Authorize(role: Role.Admin)]
    public class OdmenController : Controller
    {
        #region orders
        public ActionResult _partialCheckout()
        {
            return PartialView();
        }
        public ActionResult _partialStoreOrders()
        {
            // List<PreOrderModel> model = new StoreActions().GetOrders(Session.GetUserLkId() ?? 0);
            // return PartialView(model);
            return null;
        }
        public JsonResult GetGoods(long preorderId)
        {
            // List<PreOrderElementsModel> data = new StoreActions().GetGoods(preorderId);
            // return Json(data, JsonRequestBehavior.AllowGet);
            return null;
        }

        public JsonResult GetOrderStatus()
        {
            // List<SelectListItem> data = new StoreActions().GetOrderStatus();
            // return Json(data, JsonRequestBehavior.AllowGet);
            return null;
        }

        public JsonResult UpdateStatus(long statusId, long orderId, string statusComment)
        {
            try
            {
                //new StoreActions().UpdateStatus(orderId, statusId, statusComment);
                // List<PreOrderModel> data = new StoreActions().GetOrders(Session.GetUserLkId() ?? 0);
                //  return Json(data);
                return null;
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        public JsonResult OrdersFilter(DateTime? dateStart, DateTime? dateEnd, long statusId, string deliveryType)
        {
            // if (dateEnd != null)
            //       dateEnd = dateEnd.Value.AddHours(23);
            //    List<PreOrderModel> result = new StoreActions().StoreOrdersFilter(dateStart, dateEnd, statusId, deliveryType, Session.GetUserLkId() ?? 0);
            //   return Json(result, JsonRequestBehavior.AllowGet);
            return null;
        }
        #endregion

        public ActionResult Store(string message)
        {
            return View();
        }
        public ActionResult _partialGoods()
        {
            var store = new StoreAction();
            var model = new GoodsShortViewModel();
            model.goods = store.GetAllGoods().Select(s => new GoodsShortView { id = s.id, name = s.name, price = s.price, groupName = s.groupName }).ToList();
            return PartialView(model);
        }
        [Link(description = "Редактор товаров", url = "~/Odmen/Store")]
        public ActionResult _partialGoodsEditor(long? goodId, bool isClone)
        {
            var store = new StoreAction();
            var model = new Good();
            if (goodId > 0)
            {
                model = store.GetGoodInfo(goodId.Value);
                model.isEdit = true;
            }
            if (isClone)
            {
                model.id = 0;
                model.isEdit = false;
                model.goodProperties?.ForEach(s => s.goodId = 0);
            }
            if (model.goodProperties == null || model.goodProperties.Count == 0)
                model.goodProperties = new List<GoodProperty> { new GoodProperty() }; //чтобы была хотя бы одна пустая стркоа для заполнения
            return PartialView(model);
        }
        public JsonResult GoodDelete(long goodId)
        {
            var admin = new OdmenAction();
            admin.DeleteGood(goodId);
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateEditGood(Good model)
        {

            if (model.isEdit && model.id == 0)
                throw new Exception("Попытка редактирования товара у которого не задан Id");
            var admin = new OdmenAction();
            if (model.isEdit)
                admin.ChangeGood(model);
            else
                admin.CreateGood(model);
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult _partialCreateEditImageGood(long goodId)
        {
            if (goodId == 0)
                return Redirect("~/Error/_404");

            var store = new StoreAction();
            var good = store.GetGoodInfo(goodId);
            var model = new GoodImageEditorModel
            {
                goodId = goodId,
                name = good.name,
                images = good.images
            };
            return PartialView("_partialGoodsImageEditor", model);
        }
        [HttpPost] //////////
        public ActionResult AddGoodImages(GoodImageEditorModel model)
        {
            if (model.newImages == null || model.newImages.Count() == 0)
            {
                ModelState.AddModelError("", "Изображения не выбраны");
                return RedirectToAction(nameof(_partialCreateEditImageGood), new { goodId = model.goodId });
            }
            if (model.goodId == 0)
            {
                ModelState.AddModelError("", "Некорректный идентификатор товара");
                return RedirectToAction(nameof(_partialCreateEditImageGood), new { goodId = model.goodId });
            }
            var pathString = Server.MapPath($"~/Content/images/Goods/{model.goodId}");
            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);
            var admin = new OdmenAction();

            foreach (var file in model.newImages)
            {
                var fileName = admin.GetLastImageId();
                try
                {
                    var pathInfo = new DirectoryInfo(pathString);
                    var extention = System.IO.Path.GetExtension(file.FileName).ToLower();

                    var nameImageName = fileName + extention;
                    file.SaveAs(pathString + @"\\" + nameImageName);
                    admin.AddGoodImage(model.goodId, nameImageName);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Не удалось привязать изображение {file.FileName} {ex.Message}");
                }
            }


            return RedirectToAction(nameof(_partialCreateEditImageGood), new { goodId = model.goodId });
        }
        //////////
        public JsonResult ImageGoodDelete(long imageId)
        {
            var admen = new OdmenAction();
            admen.ImageGoodDelete(imageId);
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }
        //////////
        public JsonResult SetPrimaryImage(long imageId)
        {
            var admen = new OdmenAction();
            admen.SetPrimaryImage(imageId);
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}
