using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    internal class Account
    {
        public UserIdResult Register(RegisterModel model)
        {
            using (var db = Base.storeDataBaseContext)
            {
                string message = null;
                long? userId = null;
                var result = db.RegisterUser(model.email, model.passwordHash, model.phoneNumber, model.name, ref userId, ref message);
                if (userId > 0)
                    return new UserIdResult { userId = (long)userId };
                else
                    throw new InvalidOperationException(message ?? "Ошибка выполнения запроса");
            }
        }

        internal void SendChangePassword(UserIdModel model)
        {
            Guid? token = null;
            using (var db = Base.storeDataBaseContext)
            {
                db.GenerateToken(model.userId, ref token);
            }
            if (!token.HasValue || token.Value == Guid.Empty)
                throw new InvalidOperationException("Ошибка выполнения запроса");

            var user = Base.GetUserById(model.userId);
            var userEmail = user.email;
            var smtpSettings = SMTPSettings.Get();
            var textSubject = Base.GetSettingValue("textForgotPasswordSubject");
            var textBody = Base.GetSettingValue("textForgotPasswordBody");
            var url = Base.GetSettingValue("url");
#if DEBUG
            url = "http://localhost:3540/";
#endif
            var resetUrlLink = string.Format(url + Base.GetSettingValue("routeChangePassword"), token);
            textBody = string.Format(textBody, resetUrlLink, Environment.NewLine, user.name);
            var smtp = new SmtpClient(smtpSettings.url, smtpSettings.port)
            {
                Credentials = new NetworkCredential(smtpSettings.login, smtpSettings.password),
                EnableSsl = smtpSettings.ssl
            };
            smtp.Timeout = 5000;
            MailMessage message = new MailMessage
            {
                From = new MailAddress(smtpSettings.login, smtpSettings.alias),
                Subject = textSubject,
                Body = textBody
            };
            message.To.Add(new MailAddress(userEmail));
            smtp.Send(message);
        }

        internal UserIdResult ChangePassword(ChangePasswordModel model)
        {
            long? userId = null;
            using (var db = Base.storeDataBaseContext)
            {
                db.ChangePassword(model.token, model.passwordHash, ref userId);
            }
            if (!userId.HasValue || userId.Value == 0)
                throw new InvalidOperationException("Некорректный код подтверждения");
            return new UserIdResult { userId = (long)userId };
        }

        internal LoginResultModel LogIn(LoginModel model)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var resultLogin = new LoginResultModel();
                var result = db.IsLogin(model.email, model.passwordHash);
                if (!result.HasValue || result.Value == 0)
                {
                    resultLogin.loginResult = LoginResult.WrongLoginOrPassword;
                    return resultLogin;
                }
                resultLogin.userId = result.Value;
                if (db.UsersView.Single(s=>s.id == resultLogin.userId).emailConfirmed != true)
                {
                    resultLogin.loginResult = LoginResult.EmailIsNotConfirmed;
                    return resultLogin;

                }
                resultLogin.loginResult = LoginResult.Success;
                return resultLogin;
            }
        }

        internal UserIdResult ConfirmEmail(TokenModel model)
        {
            long? userId = null;
            using (var db = Base.storeDataBaseContext)
            {
                db.SetUserConfirmed(model.token, ref userId);
            }
            if (!userId.HasValue || userId.Value == 0)
                throw new InvalidOperationException("Некорректный код подтверждения");
            return  new UserIdResult { userId = (long)userId };
        }

        public void SendConfirmEmail(UserIdModel model)
        {
            Guid? token = null;
            using (var db = Base.storeDataBaseContext)
            {
                db.GenerateToken(model.userId, ref token);
            }
            if (!token.HasValue || token.Value == Guid.Empty)
                throw new InvalidOperationException("Ошибка выполнения запроса");

            var user = Base.GetUserById(model.userId);
            var userEmail = user.email;
            var smtpSettings = SMTPSettings.Get();
            var textSubject = Base.GetSettingValue("textEmailConfirmSubject");
            var textBody = Base.GetSettingValue("textEmailConfirmBody");
            var url = Base.GetSettingValue("url");
#if DEBUG
            url = "http://localhost:3540/";
#endif
            var resetUrlLink = string.Format(url + Base.GetSettingValue("routeEmailConfirm"), token);
            textBody = string.Format(textBody, resetUrlLink, Environment.NewLine, user.name);
            var smtp = new SmtpClient(smtpSettings.url, smtpSettings.port)
            {
                Credentials = new NetworkCredential(smtpSettings.login, smtpSettings.password),
                EnableSsl = smtpSettings.ssl
            };
            smtp.Timeout = 5000;
            MailMessage message = new MailMessage
            {
                From = new MailAddress(smtpSettings.login, smtpSettings.alias),
                Subject = textSubject,
                Body = textBody
            };       
            message.To.Add(new MailAddress(userEmail));
            smtp.Send(message);
        }

}
}
