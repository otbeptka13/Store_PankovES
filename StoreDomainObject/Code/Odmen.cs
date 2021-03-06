﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    internal class Odmen
    {
        internal long CreateGroup(GoodGroup group)
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
                group.id = groupInserting.id;
                return groupInserting.id;
            }
        }

        internal void DeleteGroup(long groupId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.GoodTypes.First(s => s.id == groupId);
                if (element != null)
                {
                    db.GoodTypes.DeleteOnSubmit(element);
                    db.SubmitChanges();
                }
            }
        }
        internal void ChangeGroup(GoodGroup group)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.GoodTypes.First(s => s.id == group.id);
                if (element != null)
                {
                    element.imageUrl = group.imageUrl;
                    element.info = group.info;
                    element.name = group.name;
                    db.SubmitChanges();
                }
            }
        }

        internal void DeleteGoodProperty(long goodPropertyId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.GoodProperties.First(s => s.id == goodPropertyId);
                if (element != null)
                {
                    db.GoodProperties.DeleteOnSubmit(element);
                    db.SubmitChanges();
                }
            }
        }

        internal void ChangeGoodProperty(GoodProperty goodProperty)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.GoodProperties.First(s => s.id == goodProperty.id);
                if (element != null)
                {
                    element.goodId = goodProperty.goodId;
                    element.value = goodProperty.value;
                    element.name = goodProperty.name;
                    db.SubmitChanges();
                }
            }
        }

        internal void CreateGoodProperties(List<GoodProperty> goodProperties)
        {
            if (goodProperties?.Count() > 0)
                foreach (var item in goodProperties)
                    CreateGoodProperty(item);
        }
        internal long CreateGoodProperty(GoodProperty goodProperty)
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
                goodProperty.id = property.id;
                return property.id;
            }
        }
        internal void DeleteGood(long goodId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Goods.First(s => s.id == goodId);
                if (element != null)
                {
                    var goodProperties = db.GoodProperties.Where(s => s.goodId == goodId);
                    if (goodProperties?.Count() > 0)
                        db.GoodProperties.DeleteAllOnSubmit(goodProperties);
                    db.Goods.DeleteOnSubmit(element);
                    db.SubmitChanges();
                }
            }
        }
        internal long CreateGood(Good good)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var goodInserting = good.ToBDObject();
                db.Goods.InsertOnSubmit(goodInserting);
                db.SubmitChanges();
                good.id = goodInserting.id;
                if (good.goodProperties?.Count > 0)
                {
                    good.goodProperties.ForEach(s => s.goodId = good.id);
                    CreateGoodProperties(good.goodProperties.Where(s => !string.IsNullOrEmpty(s.name)).ToList());

                }
                return goodInserting.id;
            }
        }

        internal void ChangeGood(Good good)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var element = db.Goods.First(s => s.id == good.id);
                if (element != null)
                {
                    element.info = good.info;
                    element.name = good.name;
                    element.discount = good.discount;
                    element.price = good.price;
                    element.typeId = good.groupId;
                    element.fullInfo = good.fullInfo ?? element.fullInfo;
                    element.brandId = good.brandId;
                    db.SubmitChanges();

                    var properties = db.GoodProperties.Where(s => s.goodId == good.id).ToList();
                    var deleted = properties.Intersect(good.goodProperties.Where(s => s.isDeleted)?.Select(g => new GoodProperties
                    {
                        goodId = g.id,
                        name = g.name,
                        value = g.value,
                        id = g.id
                    }), new GoodPropertyComparer()).ToList();
                    db.GoodProperties.DeleteAllOnSubmit(deleted);
                    var inserted = good.goodProperties.Where(s => s.id == 0 && !string.IsNullOrEmpty(s.name))?.Select(g => new GoodProperties
                    {
                        goodId = g.id,
                        name = g.name,
                        value = g.value
                    }).ToList();
                    inserted.ForEach(s => s.goodId = good.id);
                    db.GoodProperties.InsertAllOnSubmit(inserted);
                    foreach (var prop in properties)
                    {
                        var propUpdated = good.goodProperties.FirstOrDefault(s => s.id == prop.id);
                        if (propUpdated != null && !string.IsNullOrEmpty(propUpdated.name) && (prop.name != propUpdated.name || prop.value != propUpdated.value))
                        {
                            prop.name = propUpdated.name;
                            prop.value = propUpdated.value;
                        }
                    }
                    db.SubmitChanges();
                }

            }
        }
        private class GoodPropertyComparer : EqualityComparer<GoodProperties>
        {
            public override bool Equals(GoodProperties x, GoodProperties y)
            {
                return x != null && y != null && x.id == y.id && x.id > 0 ? true : false;
            }

            public override int GetHashCode(GoodProperties obj)
            {
                return (int)obj.id;
            }
        }

        internal void SetDeliverly(long packId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                db.SetDeliverly(packId);
            }
        }
    }
}
