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
    public class CustomerActionTest
    {
        private Random rand;
        [PreTest][SetUp]
        public void InitTest()
        {
            StoreDomainObject.GlobalSettings.connectionString = "Data Source=mssql6.gear.host;Initial Catalog=pankoves;Persist Security Info=True;User ID=pankoves;Password=f,shdfku";
            rand = new Random();
        }
        [Test]
        public void TestLeaveFeadback_IsNotNull()
        {
          
            var customer = new CustomerAction(8);
            var store = new StoreAction();

            var feedback = new FeedBack
            {
                goodId = store.GetAllGoods().First().id,
                mark = 3,
                message = Guid.NewGuid().ToString()
            };
            customer.LeaveFeadback(feedback);
            var feeds = store.GetFeedBack(feedback.goodId);
            var newFeedBack = feeds.FirstOrDefault(s => s.mark == feedback.mark && s.message == feedback.message && s.userId == feedback.userId);
            // TODO: Add your test code here
            Assert.IsNotNull(newFeedBack);
        }
        [Test]
        public void TestWishList()
        {
            var customer = new CustomerAction(8);
            var store = new StoreAction();
            customer.AddToWishList(3);
            customer.AddToWishList(4);
            customer.AddToWishList(5);

            var withList = customer.GetWishList();
            Assert.IsTrue(withList.Count() > 2);

            customer.DeleteWishList(3);
            Assert.IsNull(customer.GetWishList().FirstOrDefault(s=>s.id == 3));

            customer.ClearWishList();
            Assert.IsTrue(customer.GetWishList().Count() == 0);          
        }

        [Test]
        public void TestBasket()
        {
            var customer = new CustomerAction(8);
            var store = new StoreAction();
            customer.ClearBasket();

            customer.AddBasket(GenerateBasketElement());
            customer.AddBasket(GenerateBasketElement());
            Assert.IsTrue(customer.GetBasket().Count() == 2);
            customer.DelBasket(customer.GetBasket().First().id);
            Assert.IsTrue(customer.GetBasket().Count() == 1);

        }

        [Test]
        public void TestFastPay()
        {
            var customer = new CustomerAction(8);
            var store = new StoreAction();
            customer.ClearBasket();

            customer.AddBasket(GenerateBasketElement());
            customer.AddBasket(GenerateBasketElement());
            var payPackId = customer.AddBasket(GenerateBasketElement(true));
            customer.AddBasket(GenerateBasketElement());
            customer.AddBasket(GenerateBasketElement());       
            Assert.IsTrue(customer.GetBasket().Where(s=>s.packId == payPackId.packId).Count() == 1);
        }

        [Test]
        public void TestConfirmPay()
        {
            TestConfirmPayForAll(8);
        }
        [Test]
        public void Test_STRESS_PAY()
        {
            for (int i = 0; i < 100; i++)
            {
                TestConfirmPayForAll(rand.Next(20) + 1);
            }      
        }
        public void TestConfirmPayForAll(long userId)
        {
            var customer = new CustomerAction(userId);
            var store = new StoreAction();

            customer.ClearBasket();
            Assert.AreEqual(customer.GetBasket().Count(), 0);
            customer.AddBasket(GenerateBasketElement());
            customer.AddBasket(GenerateBasketElement());

            var fastPayModel = GenerateBasketElement(true);
            var fastPayId = customer.AddBasket(fastPayModel).packId;
            var packId = customer.AddBasket(GenerateBasketElement()).packId;
            var price = store.GetAllGoods().First(s => s.id == fastPayModel.goodId).price;
            var total = price * fastPayModel.count;
            var payModel = new PayModel
            {
                packId = fastPayId,
                countInBasket = 1,
                payDate = DateTime.Now,
                transactionNumber = Guid.NewGuid().ToString(),
                totalSumm = total
            };
            customer.Pay(payModel);
            Assert.AreEqual(customer.GetOrders().Where(s=>s.packId == fastPayId).Count(), 1);

            customer.AddBasket(GenerateBasketElement(true));
            customer.AddBasket(GenerateBasketElement());
            customer.AddBasket(GenerateBasketElement(true));
            Assert.AreEqual(customer.GetBasket().Count(),  4);
            Assert.IsTrue(customer.GetOrders().Count() > 0);

            var basket = customer.GetBasket();
            var payModel2 = new PayModel
            {
                packId = (long) basket.Select(s=>s.packId).Distinct().Single(),
                countInBasket = 4,
                payDate = DateTime.Now,
                transactionNumber = Guid.NewGuid().ToString(),
                totalSumm = basket.Sum(s=>s.summTotal).Value 
            };
            customer.Pay(payModel2);
            Assert.AreEqual(customer.GetOrders().Where(s => s.packId == packId).Count(), 4);

            customer.AddBasket(GenerateBasketElement());
            customer.AddBasket(GenerateBasketElement(true));
            Assert.IsTrue(customer.GetBasket().Count() == 1);
           
        }

            private AddBasketModel GenerateBasketElement(bool isFastPay = false)
        {          
            var bask = new AddBasketModel
            {
                count = ((decimal) rand.Next(10000)) / 1000,
                goodId = rand.Next(12)+1,
                isFastPay = isFastPay
            };
            if (bask.goodId == 8)
                bask.goodId = 1;
            return bask;
        }




    }
}
