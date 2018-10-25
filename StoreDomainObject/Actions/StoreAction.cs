using StoreDomainObject.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class StoreAction
    {
        public List<GoodGroup> GetGroups()
        {
            var store = new Store();
            return store.GetGroups();
        }
       // public void GetSubGroups() { }
        public List<Good> GetGoodsByGroup(long groupId)
        {
            var store = new Store();
            return store.GetGoodsByGroup(groupId);
        }
        public Good GetGoodInfo(long goodId)
        {
            var store = new Store();
            return store.GetGoodInfo(goodId);
        }
        public List<Good> GetAllGoods()
        {
            var store = new Store();
            return store.GetAllGoods();
        }
        public List<Good> PopularGoods()
        {
            var store = new Store();
            return store.PopularGoods();
        }
        public List<Good> GetNowWatching()
        {
            var store = new Store();
            return store.GetNowWatching();
        }
        public List<FeedBack> GetFeedBack(long goodId)
        {
            var store = new Store();
            return store.GetFeedBacks(goodId);
        }
    }
}
