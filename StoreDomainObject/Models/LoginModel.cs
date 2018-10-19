using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class LoginModel
    {
        public string email { get; set; }
        public string passwordHash { get; set; }
    }
    public enum LoginResult
    {
        Success, WrongLoginOrPassword, EmailIsNotConfirmed
    }
    public class LoginResultModel
    {
        public long? userId { get; set; }
        public LoginResult loginResult { get; set; }
    }
}
