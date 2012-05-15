using System;
using System.Collections.Generic;

namespace AD2.Factories
{
    internal class LoginCompleteEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public User User { get; set; }

        public LoginCompleteEventArgs(bool success, User user)
        {
            Success = success;
            User = user;
        }
    }

    internal class LoginFactory
    {
        private const string Url = "/Login";

        public delegate void LoginCompleteHandler(object sender, LoginCompleteEventArgs e);

        public void Login(string username, string password, LoginCompleteHandler handler)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData["Username"] = username;
            postData["Password"] = password;

            WebService.Post(Url, postData, delegate(bool success, string result)
                                               {
                                                   if (result == "\"Login failed.\"")
                                                   {
                                                       handler(this, new LoginCompleteEventArgs(false, null));
                                                   }
                                                   else
                                                   {
                                                       User user = Serializer.Get<User>(result);
                                                       handler(this, new LoginCompleteEventArgs(success, user));
                                                   }
                                               });
        }
    }
}
