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
    [TestFixture(Description = "CustomerActionTest")]
    public class CustomerActionTes
    {
        [PreTest]
        public void InitTest()
        {
            StoreDomainObject.GlobalSettings.connectionString = "Data Source=mssql6.gear.host;Initial Catalog=pankoves;Persist Security Info=True;User ID=pankoves;Password=f,shdfku";
        }
        [Test]
        public void TestLeaveFeadback_IsNotNull()
        {
            InitTest();
            var customer = new CustomerAction();
            var store = new StoreAction();

            var feedback = new FeedBack
            {
                goodId = store.GetAllGoods().First().id,
                userId = 8,
                mark = 3,
                message = Guid.NewGuid().ToString()
            };
            customer.LeaveFeadback(feedback);
            var feeds = store.GetFeedBack(feedback.goodId);
            var newFeedBack = feeds.FirstOrDefault(s => s.mark == feedback.mark && s.message == feedback.message && s.userId == feedback.userId);
            // TODO: Add your test code here
            Assert.IsNotNull(newFeedBack);
        }
       

    }
}
