using System;

namespace StoreDomainObject
{
    public class FeedBack
    {
        public int mark { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public string userName { get; set; }
        public long userId { get; set; }
        public long goodId { get; set; }
    }

}