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
    }
}