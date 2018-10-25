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
        public string imageUrl {get; set;}
        public short discount {get; set;}
        public string groupName {get; set;}
        public string fullInfo {get; set;}
        public string groupInfo { get;  set; }
        public List<GoodProperty> goodProperties { get; set; }
        public decimal? mark { get; set; }
        
       
        public static Good FromBDObject(Goods s)
        {
            return new Good
            {
                id = s.id,
                imageUrl = s.imageUrl,
                info = s.info,
                name = s.name,
                discount = s.discount,
                groupId = s.typeId,
                price = s.price,
                fullInfo = s.fullInfo,
                groupInfo = s.fullInfo

            };
        }
        public Goods ToBDObject()
        {
            return new Goods
            {
                id = this.id,
                imageUrl = this.imageUrl,
                info = this.info,
                name = this.name,
                discount = this.discount,
                typeId = this.groupId,
                fullInfo = this.fullInfo,
                price = this.price
            };
        }
    }

    public class GoodProperty
    {
        public long id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public long goodId { get; set; }
    }
}