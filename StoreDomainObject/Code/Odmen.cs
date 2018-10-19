using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    internal class Odmen
    {

        internal void CreateGroup(GoodGroup group)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var groupInserting = new GoodTypes
                {
                    imageUrl = group.imageUrl,
                    info = group.info,
                    name = group.name
                };
                db.GoodTypes.InsertOnSubmit(groupInserting);
                db.SubmitChanges();
            }
        }

        internal void DeleteGroup(long groupId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.GoodTypes.First(s => s.id == groupId);
                db.GoodTypes.DeleteOnSubmit(element);
                db.SubmitChanges();
            }
        }
        internal void ChangeGroup(GoodGroup group)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var updating = db.GoodTypes.First(s=>s.id == group.id);
                updating.imageUrl = group.imageUrl;
                updating.info = group.info;
                updating.name = group.name;
                db.SubmitChanges();
            }
        }
        internal void DeleteGood(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Goods.First(s => s.id == goodId);
                db.Goods.DeleteOnSubmit(element);
                db.SubmitChanges();
            }
        }
        internal void CreateGood(Good good)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goodInserting = new Goods
                {
                    imageUrl = good.imageUrl,
                    info = good.info,
                    name = good.name,
                    discount = good.discount,
                    fullInfo = good.fullInfo,
                    price = good.price,
                    typeId = good.groupId
                };
                db.Goods.InsertOnSubmit(goodInserting);
                db.SubmitChanges();
            }
        }

        internal void ChangeGood(Good good)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var updating = db.Goods.First(s => s.id == good.id);
                updating.imageUrl = good.imageUrl;
                updating.info = good.info;
                updating.name = good.name;
                updating.discount = good.discount;
                updating.price = good.price;
                updating.typeId = good.groupId;
                updating.fullInfo = good.fullInfo ?? updating.fullInfo;
                db.SubmitChanges();
            }
        }
    }
}
