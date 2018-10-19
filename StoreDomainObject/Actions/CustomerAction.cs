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
        public void AddBasket() { }
        public void DelBasket() { }
        public void ClearBasket() { }

        public void PayBasket() { }
        public void Pay() { }

        public void GetOrders() { }
        public void GetHistoryPays() { }
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
    }
}
