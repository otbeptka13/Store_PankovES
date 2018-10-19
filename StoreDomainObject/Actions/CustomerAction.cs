using StoreDomainObject.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class CustomerAction
    {
        private long userId { get; set; }
        private Customer customer { get; set; }

        public CustomerAction(long userId)
        {
            this.userId = userId;
            customer = new Customer(userId);
        }
        public ResultAddBasket AddBasket(AddBasketModel model)
        {
            return this.customer.AddBasket(model);
        }
        public List<Basket> GetBasket()
        {
            return this.customer.GetBasket();
        }
        public void DelBasket(long basketId)
        {
            customer.DelBasket(basketId);
        }
        public void ClearBasket()
        {
            customer.ClearBasket();
        }
        public void Pay(PayModel modelPay)
        {
            this.customer.Pay(modelPay);
        }
        public List<Basket> GetOrders()
        {
            return this.customer.GetOrders();
        }
        public List<Basket> GetHistoryPays() {
            return this.customer.GetHistoryPays();
        }
        public void LeaveFeadback(FeedBack feedBack)
        {       
            this.customer.LeaveFeedBack(feedBack);
        }

        public void AddToWishList(long goodId)
        {      
            this.customer.AddToWishList(goodId);
        }
        public List<Good> GetWishList()
        {
            return this.customer.GetWishList();
        }
        public void SetThatWatching(long goodId)
        {
            this.customer.SetThatWatching(goodId);
        }
        public void ClearWishList()
        {
            this.customer.ClearWishList();
        }
        public void DeleteWishList(long goodId)
        {
            this.customer.DeleteWishList(goodId);
        }
    }
}
