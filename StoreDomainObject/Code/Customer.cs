using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    internal class Customer
    {
        private long userId { get; set; }

        internal Customer(long userId)
        {
            this.userId = userId;
        }
        internal void LeaveFeedBack(FeedBack feedBack)
        {
            using (var db = Base.storeDataBaseContext)
            {
                db.LeaveFeedback(feedBack.mark, feedBack.message,feedBack.goodId, userId);
            }
        }

        internal void AddToWishList(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var wishGood = new WishList
                {
                    goodId = goodId,
                    userId = this.userId
                };
                db.WishList.InsertOnSubmit(wishGood);
                db.SubmitChanges();
            }
        }

        internal List<Good> GetWishList()
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.WishList.Where(s => s.userId == this.userId)
                    ?.Join(db.GoodsView, w => w.goodId, g => g.id, (w, g) => Good.FromBDObjectView(g))?.ToList();
            }
        }
    }
}
