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
                db.LeaveFeedback(feedBack.mark, feedBack.message, feedBack.goodId, userId);
            }
        }

        internal ResultAddBasket AddBasket(AddBasketModel model)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var result = new ResultAddBasket();
                long? packId = 0;
                db.AddBasket(this.userId, model.goodId, model.count, model.isFastPay, ref packId);
                result.packId = packId ?? 0;
                return result;
            }
        }

        internal void DelBasket(long basketId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Basket.FirstOrDefault(s => s.id == basketId && s.status == 1 && s.userId == this.userId);
                if (element != null)
                {
                    element.status = 100;
                    db.SubmitChanges();
                }
            }
        }

        internal void Pay(PayModel modelPay)
        {
            using (var db = Base.storeDataBaseContext)
            {
                db.Pay(modelPay.packId, this.userId, modelPay.countInBasket, modelPay.payDate, modelPay.transactionNumber, Math.Round(modelPay.totalSumm, 2));
            }
        }

        internal List<Basket> GetBasket()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var basket = db.Basket.Where(s => s.userId == this.userId && s.status == 1 && !s.isFastPay).ToList() ?? new List<Basket>();
                basket.ForEach(s =>
                {
                    s.name = s.Goods.name;
                    s.imageUrl = s.Goods.GoodImages.FirstOrDefault(p => p.isPrimary == true)?.imageUrl;
                    s.groupId = s.Goods.typeId;
                    s.brandName = s.Goods.GoodBrands?.name;
                });
                return basket;
            }
        }

        internal void UpdateCount(long basketId, decimal count)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Basket.First(s => s.id == basketId && s.userId == this.userId);
                if (element != null)
                {
                    element.count = count;
                    db.SubmitChanges();
                }
            }
        }

        internal List<Basket> GetOrders()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var basket = db.Basket.Where(s => s.userId == this.userId && s.status == 2).ToList() ?? new List<Basket>();
                basket.ForEach(s => s.name = s.Goods.name);
                return basket;
            }
        }

        internal List<Basket> GetHistoryPays()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var basket = db.Basket.Where(s => s.userId == this.userId && s.status == 5).ToList() ?? new List<Basket>();
                basket.ForEach(s => s.name = s.Goods.name);
                return basket;
            }
        }

        internal void ClearBasket()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var elements = db.Basket.Where(s => s.userId == this.userId && s.status == 1 && !s.isFastPay);
                if (elements?.Count() > 0)
                {
                    foreach (var item in elements)
                    {
                        item.status = 100;
                    }
                    db.SubmitChanges();
                }

            }
        }

        internal void AddToWishList(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.WishList.FirstOrDefault(s => s.userId == this.userId && s.goodId == goodId);
                if (element == null)
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
        }
        internal void ClearWishList()
        {
            using (var db = Base.storeDataBaseContext)
            {
                var elements = db.WishList.Where(s => s.userId == this.userId);
                if (elements?.Count() > 0)
                {
                    db.WishList.DeleteAllOnSubmit(elements);
                    db.SubmitChanges();
                }
            }
        }
        internal void DeleteWishList(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.WishList.FirstOrDefault(s => s.userId == this.userId && s.goodId == goodId);
                if (element != null)
                {
                    db.WishList.DeleteOnSubmit(element);
                    db.SubmitChanges();
                }
            }
        }
        internal List<Good> GetWishList()
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.WishList.Where(s => s.userId == this.userId)
                    ?.Join(db.GoodsView, w => w.goodId, g => g.id, (w, g) => new Good
                    {
                        id = g.id,
                        imageUrl = g.imageUrl,
                        info = g.info,
                        name = g.name,
                        discount = g.discount,
                        groupId = g.typeId,
                        groupInfo = g.typeInfo,
                        price = g.price,
                        groupName = g.typeName,
                        mark = g.mark

                    })?.ToList() ?? new List<Good>();
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
