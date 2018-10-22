using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;
using Web.Models;

namespace Web.Controllers
{
    [Link(description = "Главная", url = "~/")]
    public class AccountController : Controller
    {

        [HttpGet]
        [Link(description = "Авторизация")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Link(description = "Авторизация")]
        public ActionResult Login(LoginViewModel model)
        {
            var account = new AccountAction();
            LoginResultModel result;
            var accountModel = new LoginModel
            {
                email = model.email,
                passwordHash = BaseMethods.GetHashSha256(model.password)
            };

            try
            {
                result = account.LogIn(accountModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка авторизации. " + ex.Message);
                return View(model);
            }

            switch (result.loginResult)
            {
                case LoginResult.Success:
                    try
                    {
                        Session.Login(result.userId ?? 0);
                        Response.Redirect("~");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Ошибка авторизации. " + ex.Message);
                        return View(model);
                    }
                case LoginResult.WrongLoginOrPassword:
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View(model);
                case LoginResult.EmailIsNotConfirmed:
                    ModelState.AddModelError("", "Электронная почта не подтверждена. Вам была вслана ссылка для подтверждения на электронную почту " + model.email);
                    return View(model);
                default: //unreacheable
                    return View(model);
            }
        }
        [HttpGet]
        public ActionResult LogOff()
        {
            Session.LogOff();
            Response.Redirect("~");
            return null;
        }
        [HttpGet]
        [Link(description = "Регистрация")]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Link(description = "Регистрация")]
        public ActionResult Registration(RegistrationViewModel model)
        {
            var account = new AccountAction();
            var registerObject = new RegisterModel
            {
                email = model.email,
                name = model.name,
                passwordHash = BaseMethods.GetHashSha256(model.password),
                phoneNumber = model.phoneNumber
            };
            UserIdResult result;
            try
            {
                result = account.Register(registerObject);
                if (result.userId <= 0)
                    throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка регистрации пользователя. " + ex.Message);
                return View(model);
            }

            try
            {
                account.SendConfirmEmail(new UserIdModel { userId = result.userId});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка отправки подтверждения пользователя. " + ex.Message);
                return View(model);
            }
            var message = "На электронную почту " + model.email + " отправлено письмо для подтверждения регистрации";
            return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Спасибо за регистрацию!", status = StatusMessage.Mail });

        }


        [HttpGet]
        [Link(description = "Подтверждение Email")]
        public ActionResult EmailConfrimation(string token)
        {
            var account = new AccountAction();
            var tokenGuid = Guid.Empty;
            try
            {
                tokenGuid = new Guid(token);
            }
            catch 
            {
                var message = "Неверный проверочный код! Электронная почта не подтверждена";
                return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
            }
            try
            {
                var result = account.ConfirmEmail(new TokenModel { token = tokenGuid });
                if (result.userId <= 0)
                    throw new ArgumentException();
                Session.Login(result.userId);
                var message = "Электронная почта успешно подтверждена";
                return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Вы авторизованы!", status = StatusMessage.Accept });
            }
            catch
            {
                var message = "Неверный проверочный код! Электронная почта не подтверждена";
                return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
            }          
        }
        [HttpGet]
        [Link(description = "Забыли пароль?")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Link(description = "Забыли пароль?")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            var account = new AccountAction();
            string message = string.Empty;
            UserIdResult userIdResult;
            User user;
            try
            {
                userIdResult = account.GetUserIdByEmail(model.email);
                if (userIdResult.userId <= 0)
                    throw new Exception("Email не существует");
                user = account.GetUserInfo(new UserIdModel {userId = userIdResult.userId });
                if (user.emailConfirmed != true)
                {
                    message = "Сперва необходимо подтвердить электронную почту. При регистрации Вам было выслано письмо на " +model.email;
                    return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
                }
            }
            catch (Exception)
            {
                message = "Пользователь не найден!";
                return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
            }
            try
            {
                account.SendPasswordToken(new UserIdModel { userId = userIdResult.userId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка отправки подтверждения пользователя. " + ex.Message);
                return View(model);
            }
            message = "На электронную почту " + model.email + " отправлено письмо для смены пароля";
            return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Письмо отправлено!", status = StatusMessage.Mail });
            
        }


        [HttpGet]
        [Link(description = "Замена пароля")]
        public ActionResult ChangePassword(string token)
        {
            var account = new AccountAction();
            var tokenGuid = Guid.Empty;
            try
            {
                tokenGuid = new Guid(token);
            }
            catch
            {
                var message = "Неверный проверочный код!";
                return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Ошибка!", status = StatusMessage.Error });
            }

            var model = new ChangePasswordViewModel
            {
                token = tokenGuid
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Link(description = "Замена пароля")]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var account = new AccountAction();
            var changeObject = new ChangePasswordModel
            {
               token = model.token,
               passwordHash = BaseMethods.GetHashSha256(model.password)
            };
            UserIdResult result;
            try
            {
                result = account.ChangePassword(changeObject);
                if (result.userId <= 0)
                    throw new InvalidOperationException();
                Session.Login(result.userId);
                var message = "Пароль успешно изменен";
                return RedirectToAction("Info", "Home", new InfoModel { message = message, header = "Вы авторизованы!", status = StatusMessage.Accept });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка смены пароля пользователя. " + ex.Message);
                return View(model);
            }        
        }
    }
}