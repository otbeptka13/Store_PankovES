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
    [TestFixture(Description = "StoreActionTest")]
    public class StoreActionTest
    {
        [PreTest]
        public void InitTest()
        {
            StoreDomainObject.GlobalSettings.connectionString = "Data Source=mssql6.gear.host;Initial Catalog=pankoves;Persist Security Info=True;User ID=pankoves;Password=f,shdfku";
        }
        [Test]
        public void TestGetGroups_CountNotNull()
        {
            InitTest();
            var storeAction = new StoreAction();
            var result = storeAction.GetGroups();
            // TODO: Add your test code here
            Assert.IsTrue(result?.Count() > 0);
        }

        [Test]
        public void TestGetGoodsByGroup_Good()
        {
            InitTest();
            var storeAction = new StoreAction();
            var groupId = storeAction.GetGroups().OrderByDescending(s => s.id).First().id;
            var result = storeAction.GetGoodsByGroup(groupId);
            // TODO: Add your test code here
            Assert.IsTrue(result?.Count() > 0);
        }

        [Test]
        public void GetGoodInfo()
        {
            InitTest();
            var storeAction = new StoreAction();
            var rand = new Random();
            var groups = storeAction.GetGroups();
            var goods = new List<Good>();
            Good fullGood = null;
            foreach (var group in groups)
            {
                var groupGoods = storeAction.GetGoodsByGroup(group.id);
                goods.AddRange(groupGoods);
            }
            for (int i = 0; i < 10; i++)
            {
                var randomGood = goods.ElementAt(rand.Next(goods.Count()));
                fullGood = storeAction.GetGoodInfo(randomGood.id);
                
            }       
            // TODO: Add your test code here
            Assert.IsTrue(fullGood != null);
        }

        [Test]
        public void TestGetNowWatching_CountNotNull()
        {
            InitTest();
            var storeAction = new StoreAction();
            var result = storeAction.GetNowWatching();
            // TODO: Add your test code here
            Assert.IsTrue(result?.Count() > 0);
        }

        [Test]
        public void TestGetFeedBack_CountNotNull()
        {
            InitTest();
            var storeAction = new StoreAction();
            var result = storeAction.GetFeedBack(2);
            // TODO: Add your test code here
            Assert.IsTrue(result?.Count() > 0);
        }
    }
}
