using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{

    public static class SaleNewPopular
    {
        private static IEnumerable<GoodViewModel> _populars;
        public static IEnumerable<GoodViewModel> populars
        {
            get
            {
                if (lastUpdate.AddMinutes(30) < DateTime.Now)
                {
                    lastUpdate = DateTime.Now;
                    Updating();
                }
                return _populars;
            }
            set { _populars = value; }
        }

        public static IEnumerable<GoodViewModel> sales { get; private set; }
        public static IEnumerable<GoodViewModel> newes { get; private set; }
        public static DateTime lastUpdate { get; private set; }
        private static void Updating()
        {
            var store = new StoreAction();
            var goods = store.GetAllGoods().Select(s => new GoodViewModel(s)).ToList();
            sales = goods.Where(s => s.discount > 0).OrderByDescending(s => s.discount).Take(4);

            newes = goods.OrderByDescending(s => s.id).Take(4);
            foreach (var item in newes)
                item.isNew = true;
            var popularsFromAll = store.PopularGoods()?.Take(4).ToList();
            populars = goods.Where(s => popularsFromAll?.Any(w => w.id == s.id) ?? false).ToList();

            foreach (var item in populars)
                item.isBestseller = true;
        }

    }
}
