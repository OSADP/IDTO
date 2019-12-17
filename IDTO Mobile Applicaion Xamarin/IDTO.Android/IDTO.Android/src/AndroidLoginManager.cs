﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Auth;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;

using IDTO.Common;

namespace IDTO.Android
{
    class AndroidLoginManager:LoginManager
    {

        private static AndroidLoginManager instance;
        private static Context context;
        private AndroidLoginManager():base() { }

        public static AndroidLoginManager Instance(Context newcontext)
        {
                context = newcontext;

                if(instance == null)
                {
                    instance = new AndroidLoginManager();
                    
                }

                return instance;
        }

        protected override void LoadCredentials()
        {
            AccountStore acStore = AccountStore.Create(context);
            IEnumerable<Account> accounts = acStore.FindAccountsForService(SERVICE_ID);
            if (accounts.Count() > 0)
            {
                Account account = accounts.First();

                String userid = account.Properties[USER_ID_KEY];
                String usertoken = account.Properties[USER_TOKEN_KEY];

                MobileServiceUser user = new MobileServiceUser(userid);
                user.MobileServiceAuthenticationToken = usertoken;
                MobileService.CurrentUser = user;
                MobileService.UserName = account.Username;

            }

            base.LoadCredentials();
        }

		public override int GetTravelerId()
        {
            AccountStore acStore = AccountStore.Create(context);
            IEnumerable<Account> accounts = acStore.FindAccountsForService(SERVICE_ID);
            if (accounts.Count() > 0)
            {
                try
                {
                    Account account = accounts.First();

                    String travelerIdString = account.Properties[USER_TRAVELER_ID_KEY];
                    int travelerId = Int32.Parse(travelerIdString);
                    return travelerId;
                }
                catch (Exception ex)
                {
					Console.WriteLine (ex.Message);
                }
            }

            return -1;
        }

        public override string GetUserId()
        {
            AccountStore acStore = AccountStore.Create(context);
            IEnumerable<Account> accounts = acStore.FindAccountsForService(SERVICE_ID);
            if (accounts.Count() > 0)
            {
                try
                {
                    Account account = accounts.First();

                    String userId = account.Properties[USER_ID_KEY];
                    return userId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return "";
        }

		public override string GetUsername()
        {
            AccountStore acStore = AccountStore.Create(context);
            IEnumerable<Account> accounts = acStore.FindAccountsForService(SERVICE_ID);
            if (accounts.Count() > 0)
            {
                try
                {
                    Account account = accounts.First();

                    String username = account.Properties[USERNAME_ID_KEY];
                    return username;
                }
                catch (Exception ex)
                {
					Console.WriteLine (ex.Message);
                }
            }

            return "";
        }

        protected override void StoreCredentials(string username, string userid, string usertoken, int accountId)
        {
            base.StoreCredentials(username, userid, usertoken, accountId);

            Dictionary<String, String> accountOptions = new Dictionary<String, String>();
            accountOptions.Add(USER_TOKEN_KEY, usertoken);
            accountOptions.Add(USER_ID_KEY, userid);
            accountOptions.Add(USERNAME_ID_KEY, username);
            accountOptions.Add(USER_TRAVELER_ID_KEY, accountId.ToString());

            Account account = new Account(username, accountOptions);

            AccountStore acStore = AccountStore.Create(context);
            acStore.Save(account, SERVICE_ID);
        }

        protected override void ClearCredentials()
        {
            AccountStore acStore = AccountStore.Create(context);
            IEnumerable<Account> accounts = acStore.FindAccountsForService(SERVICE_ID);

            foreach (Account account in accounts)
            {
                acStore.Delete(account, SERVICE_ID);
            }
            base.ClearCredentials();
        }
    }
}
