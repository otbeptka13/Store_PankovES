using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    class Store
    {
        internal List<GoodGroup> GetGroups()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goodGroups = db.GoodTypes.Select(s => new GoodGroup
                {
                    id = s.id,
                    imageUrl = s.imageUrl,
                    info = s.info,
                    name = s.name
                }).ToList();
                return goodGroups;
            }
        }

        internal List<Good> GetGoodsByGroup(long groupId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goods = db.GoodsView.Where(s => s.typeId == groupId)?
                    .Select(s => Good.FromBDObjectView(s))?.ToList();
                return goods;
            }
        }

        internal List<FeedBack> GetFeedBacks(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var feedbacks = db.FeedBacks.Where(s => s.goodId == goodId)
                    .Join(db.UsersView, f => f.userId, u => u.id, (f, u) =>
                        new FeedBack
                        {
                            date = f.created ?? DateTime.Now,
                            mark = f.mark,
                            message = f.message,
                            userName = u.name,
                            goodId = f.goodId,
                            userId = f.userId
                        })?.ToList();
                return feedbacks;
            }
        }

        internal List<Good> GetAllGoods()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goods = db.GoodsView.Select(s => Good.FromBDObjectView(s))?.ToList();
                return goods;
            }
        }
        internal List<Good> GetNowWatching()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goods = db.GetNowWatching()
                    .Select(s => new Good
                    {
                        id = s.id,
                        imageUrl = s.imageUrl,
                        info = s.info,
                        name = s.name,
                        discount = s.discount,
                        groupId = s.typeId,
                        groupInfo = s.typeInfo,
                        price = s.price,
                        groupName = s.typeName

                    })?.ToList();
                return goods;
            }
        }

        internal Good GetGoodInfo(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var good = db.GoodsView.Select(s => Good.FromBDObjectView(s)).FirstOrDefault(s => s.id == goodId);
                good.fullInfo = db.GetFullInfo(goodId);
                var goodProperties = db.GoodProperties.Where(s => s.goodId == goodId).Select(s => new GoodProperty
                {
                    goodId = s.goodId,
                    name = s.name,
                    value = s.value,
                    id = s.id
                }).ToList();
                good.goodProperties = goodProperties;
                db.AddNowWatching(goodId);
                return good;
            }
        }
    }
}
