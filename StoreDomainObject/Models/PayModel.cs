using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class PayModel
    {
        public long packId { get; set; }
        public int countInBasket { get; set; } //for validate
        public DateTime payDate { get; set; }
        public string transactionNumber { get; set; }
    }
    public class AddBasketModel
    {
        public bool isFastPay { get; set; }
        public long goodId { get; set; } 
        public decimal count { get; set; }

    }
    public class ResultAddBasket
    {
        public long packId { get; set; }

    }
}
