using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Link(description = "Каталог", url = "~/Catalog")]
    public class StoreController : Controller
    {
        [Route("~/Details/{goodTypeId}/{goodId:int}")]
        public ActionResult Details(long goodId)
        {
            try
            {
                var store = new StoreAction();
                var good = store.GetGoodInfo(goodId);
                var model = new GoodDetailsViewModel(good);
                model.feedbacks = store.GetFeedBack(goodId);
                model.canSendFeedback = Session.IsAuth() && !model.feedbacks.Any(s => s.userId == Session.GetUserId());
                if (Session.IsAuth())
                {
                    var customer = new CustomerAction(Session.GetUserId());
                    customer.SetThatWatching(goodId);
                }
                SetPratentGroupLinks(good.groupId);
                (ViewBag.Links as Queue<Link>).Enqueue(new Link { description = good.name, url = $"~/Details/{good.groupId}/{good.id}" });
                return View(model);
            }
            catch (Exception ex)
            {
                HttpContext.Response.Redirect("/Error/_404", true);
                return null;
            }

        }
        [Route("~/Catalog/{groupId}")]
        [Route("~/Catalog")]
        public ActionResult Catalog(long? groupId)
        {
            var model = GetCatalogModel(groupId);
            return View(model);
        }
        public ActionResult FilterDo(FilterCatalogModel filter)
        {
            var model = GetCatalogModel(filter.groupId);
            model.goods = model.goods.Where(s => s.price >= filter.selectedMinPrice && s.price <= filter.selectedMaxPrice).
                Join(filter.brands.Where(s => s.isChecked), g => g.brandId, b => b.brandId, (g, b) => g).ToList();
            model.filter.selectedMaxPrice = filter.selectedMaxPrice;
            model.filter.selectedMinPrice = filter.selectedMinPrice;
            filter.brands.Where(s=>!s.isChecked).ToList()?.ForEach(b =>  model.filter.brands.FirstOrDefault(s=>s.brandId == b.brandId).isChecked = false);
            return View("Catalog", model);
        }

        private CatalogModel GetCatalogModel(long? groupId)
        {
            var store = new StoreAction();
            if (groupId == null || groupId == 0)
            {
                var modelAll =  new CatalogModel
                {
                    goods = store.GetAllGoods().Select(s=> new GoodViewModel(s)).ToList(),
                    groupId = 0,

                };
                modelAll.filter = _filterCatalog(modelAll);
                return modelAll;
            }
            var allGroups = store.GetGroups();
            var groups = new List<long>();
            if (!allGroups.Exists(s => s.id == groupId))
            {
                Response.Redirect(Url.Content("~/Error/_404"),true);
                return null;
            }
            SetPratentGroupLinks(groupId.Value);
            GetChildGroups(groupId.Value, ref groups);
            var goods = store.GetAllGoods().Join(groups, good => good.groupId, group => group, (good, group) => new GoodViewModel(good)).ToList();
            var model = new CatalogModel
            {
                goods = goods
            };
            model.groupId = groupId.Value;
            model.filter = _filterCatalog(model);
            return model;
        }
        private FilterCatalogModel _filterCatalog(CatalogModel model)
        {
            var store = new StoreAction();
            var filterModel = new FilterCatalogModel();
            filterModel.groupId = model.groupId;
            if (model.goods == null || model.goods.Count == 0)
                return filterModel;
            filterModel.minPrice = filterModel.selectedMinPrice = model.goods.Min(s => s.price);
            filterModel.maxPrice = filterModel.selectedMaxPrice = model.goods.Max(s => s.price);
            var brands = model.goods.Select(s => s.brandId).Where(s=>s.HasValue).Distinct().ToList();
            filterModel.brands = new List<BrandFilter>();
            var brandsAll = store.GetGoodBrands();
            if (brands?.Count() > 0)
                foreach (var item in brands)
                {
                    var brand = brandsAll.Single(s => s.id == item);
                    filterModel.brands.Add(new BrandFilter
                    {
                        brandId = brand.id,
                        brandName = brand.name,
                        brandCount = model.goods.Count(s => s.brandId == brand.id),
                        isChecked = true
                    });
                }
            return filterModel;
        }
        private void SetPratentGroupLinks(long groupId)
        {
            var que = new Queue<Link>();
            que.Enqueue(GetGroupCatalogLink(groupId));
            var store = new StoreAction();
            long? parentId = store.GetGroups().FirstOrDefault(s => s.id == groupId).parentId;
            while (parentId != null)
            {
                var group = store.GetGroups().FirstOrDefault(s => s.id == parentId);
                que.Enqueue(GetGroupCatalogLink(group.id));
                parentId = store.GetGroups().FirstOrDefault(s => s.id == group.parentId)?.parentId;
            }
            foreach (var item in que.Reverse())
            {
                (ViewBag.Links as Queue<Link>).Enqueue(item);
            }
        }
        private void GetChildGroups(long groupId, ref List<long> groups)
        {
            groups.Add(groupId);
            var store = new StoreAction();
            var childGroups = store.GetGroups().Where(s => s.parentId == groupId).Select(s => s.id).ToList();
            if (childGroups?.Count() > 0)
                foreach (var id in childGroups)
                    GetChildGroups(id, ref groups);
        }

        private Link GetGroupCatalogLink(long groupId)
        {
            var group = new StoreAction().GetGroups().Single(s => s.id == groupId);
            return new Link { description = group.name, url = "~/Catalog/" + group.id };
        }

        public ActionResult Searching(SearchingModel model)
        {
            var result = GetCatalogModel(model.groupId);
            result.goods = result.goods.Where(s => s.name.ToUpper().Contains(model.search?.ToUpper())).ToList();
            result.filter = _filterCatalog(result);
            return View("Catalog", result);
        }
    }


}
