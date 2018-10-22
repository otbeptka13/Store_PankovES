using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class User
    {
        public long id { get; set; }
        public string email { get; set; }
        public bool? emailConfirmed { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? dateRegistration { get; set; }
        public string name { get; set; }
        public string role { get; set; }

        public List<Good> wishList { get; set; }
        public List<Basket> basket { get; set; }
    }
}
