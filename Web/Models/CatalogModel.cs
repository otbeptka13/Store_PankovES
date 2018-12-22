using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CatalogModel
    {
        public long groupId { get; set; }
        public List<GoodViewModel> goods { get; set; }
        public FilterCatalogModel filter { get; set; }
        public List<CatalogGroupTree> catalogTree
        {
            get
            {
                var storeActions = new StoreAction();
                var allGroups = storeActions.GetGroups();
                var root = allGroups.Where(s => s.parentId == null);
                List<CatalogGroupTree> tree = null;
                foreach (var item in root)
                {
                    FindChild(ref tree, item);
                }
                return tree;
            }
        }

        private void FindChild(ref List<CatalogGroupTree> catalogTree, GoodGroup group)
        {
            var storeActions = new StoreAction();
            var parentId = group.id;
            if (catalogTree == null)
                catalogTree = new List<CatalogGroupTree>();
            var current = new CatalogGroupTree { groupId = group.id, groupName = group.name,
            link = $"~/Catalog/{group.id}"};
            catalogTree.Add(current);
            var groups = storeActions.GetGroups().Where(s => s.parentId == parentId).ToList();
            if (groups?.Count > 0)
                foreach (var item in groups)
                {
                    FindChild(ref current.childGroups, item);
                }
        }
    }
    public class CatalogGroupTree
    {
        public long groupId { get; set; }
        public string groupName { get; set; }
        public string link { get; set; }
        public List<CatalogGroupTree> childGroups;
    }

    public class FilterCatalogModel
    {
        public long groupId { get; set; }
        public List<BrandFilter> brands { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public decimal selectedMinPrice { get; set; }
        public decimal selectedMaxPrice { get; set; }
    }
    public class BrandFilter
    {
        public long brandId { get; set; }
        public long brandCount { get; set; }
        public string brandName { get; set; }
        public bool isChecked { get; set; }
    }

}