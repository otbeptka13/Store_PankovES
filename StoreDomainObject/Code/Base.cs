using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDomainObject.Code
{
    class Base
    {
        public static StoreDataBaseDataContext storeDataBaseContext => new StoreDataBaseDataContext(GlobalSettings.connectionString);

        public static string GetSettingValue(string settingName)
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.Settings.Single(s => s.name.ToUpper()  == settingName.ToUpper()).value;
            }
        }
        public static string GetUserEmailById(long userId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.UsersView.Single(s => s.id == userId).email;
            }
        }

        public static User GetUserById(long userId)
        {
            using (var db = Base.storeDataBaseContext)
            {
                return db.UsersView.Select(s=> new User {
                    id = s.id,
                    dateRegistration = s.dateRegistration,
                    email = s.email,
                    emailConfirmed = s.emailConfirmed,
                    phoneNumber = s.phoneNumber,
                    role = s.role,
                    name = s.name
                    })
                .Single(s => s.id == userId);
            }
        }

        internal static UserIdResult GetUserIdByEmail(string email)
        {
            using (var db = Base.storeDataBaseContext)
            {
                var id =  db.UsersView.FirstOrDefault(s => s.email == email)?.id ?? 0;
                return new UserIdResult { userId = id };
            }
        }
    }
}
