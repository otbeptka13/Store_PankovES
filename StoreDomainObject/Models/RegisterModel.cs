using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class RegisterModel
    {
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
    }
    public class UserIdResult : BaseResult
    {
        public long userId { get; set; }
    }
    public class UserIdModel
    {
        public long userId { get; set; }
    }

    public class TokenModel
    {
        public Guid token { get; set; }
    }
    public class ChangePasswordModel
    {
        public Guid token { get; set; }
        public string passwordHash { get; set; }
    }
}
