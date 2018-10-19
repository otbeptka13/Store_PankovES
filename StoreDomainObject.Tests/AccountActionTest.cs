using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDomainObject;
using System.Security.Cryptography;

namespace StoreDomainObject.Tests
{
    [TestFixture(Description = "AccountActionTest")]
    public class AccountActionTest
    {
        [PreTest]
        public void InitTest()
        {
            StoreDomainObject.GlobalSettings.connectionString = "Data Source=mssql6.gear.host;Initial Catalog=pankoves;Persist Security Info=True;User ID=pankoves;Password=f,shdfku";
        }
        [Test]
        public void TestRegister_NewEmail()
        {
            InitTest();
            var model = new RegisterModel
            {
                email = "Egor-1313@yandex.ru" + Guid.NewGuid(),
                passwordHash = GetHashSha256("test"),
                phoneNumber = "79041840611",
                name = "TEST TESTOVICH"
                
            };
            var result = new AccountAction().Register(model);
            // TODO: Add your test code here
            Assert.IsTrue(result.userId > 0);
        }
        [Test]
        public void TestRegister_BusyEmail()
        {
            InitTest();
            var model = new RegisterModel
            {
                email = "Egor-1313@yandex.ru",
                passwordHash = GetHashSha256("qwerty1"),
                phoneNumber = "79041840611"
            };
            //   var result = new AccountAction().Register(model);
            // TODO: Add your test code here
            Assert.Throws<InvalidOperationException>(() => { new AccountAction().Register(model); });
        }

        [Test]
        public void TestLogin_Good()
        {
            InitTest();
            var model = new LoginModel
            {
                email = "Egor-1313@yandex.ru",
                passwordHash = GetHashSha256("qwerty1")
            };
            //   var result = new AccountAction().Register(model);
            // TODO: Add your test code here
            var result = new AccountAction().LogIn(model);
            // TODO: Add your test code here
            Assert.IsTrue(result.userId > 0 && result.loginResult == LoginResult.Success);
        }

        [Test]
        public void TestLogin_NotConfirmed()
        {
            InitTest();
            var model = new RegisterModel
            {
                email = "Egor-1313@yandex.ru" + Guid.NewGuid(),
                passwordHash = GetHashSha256("test" + Guid.NewGuid()),
                phoneNumber = "79041840611"
            };
            var loginModel = new LoginModel
            {
                email = model.email,
                passwordHash = model.passwordHash
            };
            new AccountAction().Register(model);
            //   var result = new AccountAction().Register(model);
            // TODO: Add your test code here
            var result = new AccountAction().LogIn(loginModel);
            // TODO: Add your test code here
            Assert.IsTrue(result.userId > 0 && result.loginResult == LoginResult.EmailIsNotConfirmed);
        }
        [Test]
        public void TestLogin_WrongPassword()
        {
            InitTest();           
            var loginModel = new LoginModel
            {
                email = "Egor-1313@yandex.ru",
                passwordHash = GetHashSha256(Guid.NewGuid().ToString())
            };
            // TODO: Add your test code here
            var result = new AccountAction().LogIn(loginModel);
            // TODO: Add your test code here
            Assert.IsTrue(!(result.userId > 0) && result.loginResult == LoginResult.WrongLoginOrPassword);
        }

        [Test]
        public void TestLogin_WrongAll()
        {
            InitTest();
            var loginModel = new LoginModel
            {
                email = Guid.NewGuid().ToString(),
                passwordHash = GetHashSha256(Guid.NewGuid().ToString())
            };
            // TODO: Add your test code here
            var result = new AccountAction().LogIn(loginModel);
            // TODO: Add your test code here
            Assert.IsTrue(!(result.userId > 0) && result.loginResult == LoginResult.WrongLoginOrPassword);
        }

        [Test]
        public void TestSendEmailConfirm_Good()
        {
            InitTest();
            var userIdModel = new UserIdModel
            {
                userId = 8
            };
            // TODO: Add your test code here
            new AccountAction().SendConfirmEmail(userIdModel);
            // TODO: Add your test code here
          //  Assert.(!(result.userId > 0) && result.loginResult == LoginResult.WrongLoginOrPassword);
        }

        [Test]
        public void TestEmailSetConfirm_Good()
        {
            InitTest();
            var token = new TokenModel
            {
                token = new Guid("62c575df-64a3-4d02-8924-abe1a07fcb37")
            };
            // TODO: Add your test code here
            var result = new AccountAction().ConfirmEmail(token);
            // TODO: Add your test code here
            Assert.True(result.userId > 0);
        }
        [Test]
        public void TestSendPasswordToken_Good()
        {
            InitTest();
            var userIdModel = new UserIdModel
            {
                userId = 8
            };
            // TODO: Add your test code here
            new AccountAction().SendPasswordToken(userIdModel);
            // TODO: Add your test code here
            //  Assert.(!(result.userId > 0) && result.loginResult == LoginResult.WrongLoginOrPassword);
        }
        [Test]
        public void TestChangePassword_Good()
        {
            InitTest();
            var token = new ChangePasswordModel
            {
                token = new Guid("A178A744-A033-444A-96DB-A9D56C6E146A"),
                passwordHash = GetHashSha256("qwerty1")

            };
            // TODO: Add your test code here
            var result = new AccountAction().ChangePassword(token);
            // TODO: Add your test code here
            Assert.True(result.userId > 0);
        }
        private static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
