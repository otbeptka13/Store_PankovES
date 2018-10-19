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
        public void AddBasket() { }
        public void DelBasket() { }
        public void ClearBasket() { }

        public void PayBasket() { }
        public void Pay() { }

        public void GetOrders() { }
        public void GetHistoryPays() { }

        public void LeaveFeadback(FeedBack feedBack)
        {
            var customer = new Customer();
            customer.LeaveFeedBack(feedBack);
        }
    }
}
