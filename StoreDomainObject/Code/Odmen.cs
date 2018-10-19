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

        internal void DeleteGoodProperty(long goodPropertyId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.GoodProperties.First(s => s.id == goodPropertyId);
                db.GoodProperties.DeleteOnSubmit(element);
                db.SubmitChanges();
            }
        }

        internal void ChangeGoodProperty(GoodProperty goodProperty)
        {
             using (var db = Base.storeDataBaseContext)
            {
                var updating = db.GoodProperties.First(s => s.id == goodProperty.id);
                updating.goodId = goodProperty.goodId;
                updating.value = goodProperty.value;
                updating.name = goodProperty.name;
                db.SubmitChanges();
            }
        }

        internal void CreateGoodProperty(GoodProperty goodProperty)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var property = new GoodProperties
                {
                    goodId = goodProperty.goodId,
                    name = goodProperty.name,
                    value = goodProperty.value,
                };
                db.GoodProperties.InsertOnSubmit(property);
                db.SubmitChanges();
            }
        }

        internal void DeleteGood(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Goods.First(s => s.id == goodId);
                var goodProperties = db.GoodProperties.Where(s => s.goodId == goodId);
                db.GoodProperties.DeleteAllOnSubmit(goodProperties);
                db.Goods.DeleteOnSubmit(element);
                db.SubmitChanges();
            }
        }
        internal void CreateGood(Good good)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goodInserting = good.ToBDObject();
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
