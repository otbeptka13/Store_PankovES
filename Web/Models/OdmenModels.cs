using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{

    public class GoodsShortViewModel
    {
        public List<GoodsShortView> goods { get; set; }
        public string message { get; set; }
    }
    public class GoodsShortView
    {
        public long id { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public string groupName { get; set; }
    }

    public class GoodImageEditorModel
    {
        public long goodId { get; set; }
        public string name { get; set; }
        public List<GoodImages> images { get; set; }
        public HttpPostedFileBase[] newImages { get; set; }
        public long primaryImageId { get; set; }
        public const int MAX_IMAGES_COUNT = 5;

    }
}