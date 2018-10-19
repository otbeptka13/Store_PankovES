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

        internal ResultAddBasket AddBasket(AddBasketModel model)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var result = new  ResultAddBasket();
                long? packId = 0;
                db.AddBasket(this.userId,model.goodId, model.count, model.isFastPay, ref packId);
                result.packId = packId ?? 0;
                return result;
            }
        }

        internal void DelBasket(long basketId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Basket.FirstOrDefault(s => s.id == basketId && s.status == 1);
                element.status = 100;
                db.SubmitChanges();
            }
        }

        internal void Pay(PayModel modelPay)
        {
            using (var db = Base.storeDataBaseContext)
            {
                db.Pay(modelPay.packId,this.userId,modelPay.countInBasket,modelPay.payDate, modelPay.transactionNumber);
            }
        }

        internal List<Basket> GetOrders()
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.Basket.Where(s => s.userId == this.userId && s.status == 2).ToList();
            }
        }

        internal List<Basket> GetHistoryPays()
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.Basket.Where(s => s.userId == this.userId && s.status == 5).ToList();
            }
        }

        internal void ClearBasket()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var elements = db.Basket.Where(s => s.userId == this.userId && s.status == 1);
                if (elements?.Count() > 0)
                {
                    foreach (var item in elements)
                    {
                        item.status = 100;
                    }
                }
                db.SubmitChanges();
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
        internal void SetThatWatching(long goodId)
        {
            
            using (var db = Base.storeDataBaseContext)
            {
                db.AddNowWatching(goodId, this.userId); 
            }
        }
    }
}
