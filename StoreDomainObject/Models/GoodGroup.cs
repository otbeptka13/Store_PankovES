namespace StoreDomainObject
{
    public class GoodGroup
    {
        public long id {get; set;}
        public string name {get; set;}
        public string info {get; set;}
        public string imageUrl {get; set;}
        public long? parentId { get; set; }
    }
}