using System.Collections.Generic;

namespace StoreDomainObject
 {
    public class Good
     {
        public long id {get; set;}
        public string name {get; set;}
        public long groupId {get; set;}
        public decimal price {get; set;}
        public string info {get; set;}
        public string imageUrl {get; set;} //primaryImage
        public short discount {get; set;}
        public string groupName {get; set;}
        public string fullInfo {get; set;}
        public string groupInfo { get;  set; }
        public long? brandId { get; set; }
        public string brandName { get; set; }
        public List<GoodProperty> goodProperties { get; set; }
        public List<GoodImages> images { get; set; }
        public decimal? mark { get; set; }
        public bool isEdit { get; set; }


        public static Good FromBDObject(Goods s)
        {
            return new Good
            {
                id = s.id,
                info = s.info,
                name = s.name,
                discount = s.discount,
                groupId = s.typeId,
                price = s.price,
                fullInfo = s.fullInfo,
                groupInfo = s.fullInfo,
                brandId = s.brandId
            };
        }
        public Goods ToBDObject()
        {
            return new Goods
            {
                id = this.id,
                info = this.info,
                name = this.name,
                discount = this.discount,
                typeId = this.groupId,
                fullInfo = this.fullInfo,
                price = this.price,
                brandId = this.brandId
            };
        }
    }

    public class GoodProperty
    {
        public long id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public long goodId { get; set; }
        public bool isDeleted { get; set; }
    }
}