using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodViewModel : Good
    {
        public bool isBestseller { get; set; }
        public bool isNew { get; set; }
        public decimal previousPrice => Math.Round(price * 100 / (100 - discount), 2);
        public bool isSale => discount > 0;
        public GoodViewModel(Good good)
        {
            id = good.id;
            name = good.name;
            groupId = good.groupId;
            price = good.price;
            info = good.info;
            imageUrl = good.imageUrl;
            discount = good.discount;
            groupName = good.groupName;
            fullInfo = good.fullInfo;
            groupInfo = good.groupInfo;
            goodProperties = good.goodProperties;
            images = good.images;
            mark = good.mark;
            brandId = good.brandId;
            brandName = good.brandName;

        }
    }


    public class GoodDetailsViewModel : GoodViewModel
    {
        public GoodDetailsViewModel(Good good) : base(good)
        {
        }
        public List<FeedBack> feedbacks { get; set; }
        public bool canSendFeedback { get; set; }
    }


}