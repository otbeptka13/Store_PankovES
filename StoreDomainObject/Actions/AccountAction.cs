using StoreDomainObject.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject
{
    public class AccountAction
    {
        public UserIdResult Register(RegisterModel model)
        {
            var account = new Account();
            return account.Register(model);
        }
        public LoginResultModel LogIn(LoginModel model) {
            var account = new Account();
            return account.LogIn(model);
        }
        public void LogOff() { }

        public void SendPasswordToken(UserIdModel model)
        {
            var account = new Account();
            account.SendChangePassword(model);
        }
        public UserIdResult ChangePassword(ChangePasswordModel model)
        {
            var account = new Account();
            return account.ChangePassword(model);
        }

        public void SendConfirmEmail(UserIdModel model) {
            var account = new Account();
            account.SendConfirmEmail(model);

        }
        public UserIdResult ConfirmEmail(TokenModel model) {
            var account = new Account();
            return account.ConfirmEmail(model);
        }

        public User GetUserInfo(UserIdModel model)
        {        
            return Base.GetUserById(model.userId);
        }

        public UserIdResult GetUserIdByEmail(string email)
        {
            return Base.GetUserIdByEmail(email);
        }

    }
}
