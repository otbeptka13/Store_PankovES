﻿using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public static class SessionExtention
    {

        public static bool IsAuth(this HttpSessionStateBase sbase) => (User)sbase["user"] != null;
        public static User GetUser(this HttpSessionStateBase sbase) => (User)sbase["user"];
        private static void SetUser(this HttpSessionStateBase sbase, User user) => sbase["user"] = user;

        public static void Login(this HttpSessionStateBase sbase, long userId)
        {
            var account = new AccountAction();
            var user = account.GetUserInfo(new UserIdModel { userId = userId });
            var customer = new CustomerAction(userId);
            user.basket = customer.GetBasket();
            user.wishList = customer.GetWishList();
            SetUser(sbase, user);
        }
        public static void LogOff(this HttpSessionStateBase sbase)
        {
            var account = new AccountAction();
            account.LogOff();         
            SetUser(sbase, null);
        }
    }
}