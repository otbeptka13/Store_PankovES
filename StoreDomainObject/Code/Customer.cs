using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    internal class Customer
    {
        internal void LeaveFeedBack(FeedBack feedBack)
        {
            using (var db = Base.storeDataBaseContext)
            {
                db.LeaveFeedback(feedBack.mark, feedBack.message,feedBack.goodId, feedBack.userId);
            }
        }
    }
}
