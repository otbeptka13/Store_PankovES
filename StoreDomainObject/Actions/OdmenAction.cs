using StoreDomainObject.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class OdmenAction
    {
        private Odmen odmen;
        public OdmenAction()
        {
            odmen = new Odmen();
        }
        public long CreateGroup(GoodGroup group)
        {
            return this.odmen.CreateGroup(group);
        }
        public void DeleteGroup(long groupId)
        {
            this.odmen.DeleteGroup(groupId);
        }
        public void ChangeGroup(GoodGroup group)
        {
            this.odmen.ChangeGroup(group);
        }

        public long CreateGood(Good good)
        {
            return this.odmen.CreateGood(good);
        }
        public void DeleteGood(long goodId)
        {
            this.odmen.DeleteGood(goodId);
        }
        public void ChangeGood(Good good)
        {
            this.odmen.ChangeGood(good);
        }
        public long CreateGoodProperty(GoodProperty goodProperty)
        {
            return this.odmen.CreateGoodProperty(goodProperty);
        }
        public void CreateGoodProperties(List<GoodProperty> goodProperty)
        {
            this.odmen.CreateGoodProperties(goodProperty);
        }
        public void DeleteGoodProperty(long goodPropertyId)
        {
            this.odmen.DeleteGoodProperty(goodPropertyId);
        }
        public void ChangeGoodProperty(GoodProperty goodProperty)
        {
            this.odmen.ChangeGoodProperty(goodProperty);
        }
        public void SetDeliverly(long packId)
        {
            this.odmen.SetDeliverly(packId);
        }

        public object GetLastImageId()
        {
            using (var db = new StoreDataBaseDataContext())
            {
                return (db.GoodImages?.Max(s => s.id) + 1) ?? 1;
            }
        }

        public void AddGoodImage(long goodId, string imageName)
        {
            using (var db = new StoreDataBaseDataContext())
            {
                var isPrimary = db.GoodImages.Count(s => s.goodId == goodId) == 0;
                var goodImage = new GoodImages
                {
                    goodId = goodId,
                    imageUrl = imageName,
                    isPrimary = isPrimary
                };
                db.GoodImages.InsertOnSubmit(goodImage);
                db.SubmitChanges();            
            }
        }

        public void ImageGoodDelete(long imageId)
        {
            using (var db = new StoreDataBaseDataContext())
            {
                var image = db.GoodImages.First(s => s.id == imageId);
                db.GoodImages.DeleteOnSubmit(image);
                db.SubmitChanges();
            }
        }

        public void SetPrimaryImage(long imageId)
        {
            using (var db = new StoreDataBaseDataContext())
            {
                var image = db.GoodImages.First(s => s.id == imageId);
                var imageOldPrimary = db.GoodImages.FirstOrDefault(s => s.goodId == image.goodId && s.isPrimary == true);
                if (imageOldPrimary != null)
                    imageOldPrimary.isPrimary = false;
                image.isPrimary = true;
                db.SubmitChanges();
            }
        }
    }
}
