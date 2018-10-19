using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDomainObject;
using System.Security.Cryptography;

namespace StoreDomainObject.Tests
{
    [TestFixture(Description = "OdmenActionTest")]
    public class OdmenActionTest
    {
        [PreTest][SetUp]
        public void InitTest()
        {
            StoreDomainObject.GlobalSettings.connectionString = "Data Source=mssql6.gear.host;Initial Catalog=pankoves;Persist Security Info=True;User ID=pankoves;Password=f,shdfku";
        }
        [Test]
        public void TestCreateGroup()
        {
            
            var odmen = new OdmenAction();
            var store = new StoreAction();

            var newGroup = new GoodGroup
            {
                imageUrl = "image" + Guid.NewGuid().ToString(),
                info = "info" + Guid.NewGuid().ToString(),
                name = "name" + Guid.NewGuid().ToString().Substring(0, 10)
            };
            var id = odmen.CreateGroup(newGroup);
            
            Assert.IsTrue(id > 0);
        }
        [Test]
        public void TestDeleteGroup()
        {
            
            var odmen = new OdmenAction();
            var store = new StoreAction();
            var groupsForDel = store.GetGroups().Where(s => s.name.Contains("name"));
            if (!(groupsForDel?.Count()> 0))
            {
                Assert.Pass("Нет тестовой группы для удаления");
                return;
            }
            foreach (var groupForDel in groupsForDel)
                odmen.DeleteGroup(groupForDel.id);

            var deleted = store.GetGroups().FirstOrDefault(s => s.name.Contains("name"));
            // TODO: Add your test code here
            Assert.IsNull(deleted);
        }

        [Test]
        public void TestUpdateGroup()
        {
            
            var odmen = new OdmenAction();
            var store = new StoreAction();
            var groupUpdate = store.GetGroups().FirstOrDefault(s => s.name.Contains("name"));
            if (groupUpdate == null)
            {
                Assert.Pass("Нет тестовой группы для изменения");
                return;
            }
            groupUpdate.name = "name_Updated";
            groupUpdate.info = "info_Updated";
            groupUpdate.imageUrl = "imageUrl_Updated";

            odmen.ChangeGroup(groupUpdate);
            var updated = store.GetGroups().FirstOrDefault(s => s.name == groupUpdate.name
                                    && s.info == groupUpdate.info && s.imageUrl == groupUpdate.imageUrl);
            // TODO: Add your test code here
            Assert.IsNotNull(updated);
        }
        [Test]
        public void TestCreateGood()
        {
            
            var odmen = new OdmenAction();
            var store = new StoreAction();
            var groupId = store.GetGroups().FirstOrDefault().id;
            var newGood = new Good
            {
                imageUrl = "image" + Guid.NewGuid().ToString(),
                info = "info" + Guid.NewGuid().ToString(),
                name = "name" + Guid.NewGuid().ToString().Substring(0, 10),
                discount = 13,
                groupId = groupId,
                price = 123.44M,
                fullInfo = Guid.NewGuid().ToString() + Guid.NewGuid().ToString()
            };
            var id = odmen.CreateGood(newGood);
            var property = new GoodProperty
            {
                goodId = id,
                name = "name" + Guid.NewGuid(),
                value = "value" + Guid.NewGuid()
            };
            var properties = new List<GoodProperty> {new GoodProperty
            {
                goodId = id,
                name = "name" + Guid.NewGuid(),
                value = "value" + Guid.NewGuid()
            },new GoodProperty
            {
                goodId = id,
                name = "name" + Guid.NewGuid(),
                value = "value" + Guid.NewGuid()
            }
            };
            var propertyid = odmen.CreateGoodProperty(property);
            odmen.CreateGoodProperties(properties);
            Assert.IsTrue(id > 0 && propertyid > 0 && property.id > 0  && !properties.Any(s=> s.id == 0));
        }
        [Test]
        public void TestDeleteGood()
        {
            
            var odmen = new OdmenAction();
            var store = new StoreAction();
            var goods = store.GetAllGoods().Where(s => s.name.Contains("name"));
            if (!(goods?.Count() > 0))
            {
                Assert.Pass("Нет тестового товара для удаления");
                return;
            }
            foreach (var goodForDel in goods)
                odmen.DeleteGood(goodForDel.id);

            var deleted = store.GetAllGoods().FirstOrDefault(s => s.name.Contains("name"));
            // TODO: Add your test code here
            Assert.IsNull(deleted);
        }
        [Test]
        public void TestUpdateGood()
        {
            
            var odmen = new OdmenAction();
            var store = new StoreAction();
            var goodUpdate = store.GetAllGoods().FirstOrDefault(s => s.name.Contains("name"));
            if (goodUpdate == null)
            {
                Assert.Pass("Нет тестового товара для изменения");
                return;
            }
            goodUpdate.name = "name_Updated";
            goodUpdate.info = "info_Updated";
            goodUpdate.imageUrl = "imageUrl_Updated";
            goodUpdate.discount = 78;
            goodUpdate.price = 67889.23M;
            goodUpdate.groupId = store.GetGroups().Last().id;
            goodUpdate.fullInfo = "fullInfo_Updated";
            odmen.ChangeGood(goodUpdate);
            var updated = store.GetAllGoods().FirstOrDefault(s => s.name == goodUpdate.name 
                                    //&& s.fullInfo == goodUpdate.fullInfo
                                    && s.info == goodUpdate.info && s.imageUrl == goodUpdate.imageUrl
                                    && s.price == goodUpdate.price && s.discount == goodUpdate.discount);
            // TODO: Add your test code here
            Assert.IsNotNull(updated);
        }

    }
}
