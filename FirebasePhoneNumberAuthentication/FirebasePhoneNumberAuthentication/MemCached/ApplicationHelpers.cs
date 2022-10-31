using FirebasePhoneNumberAuthentication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FirebasePhoneNumberAuthentication.MemCached
{
    public class Authenticator
    {
        public static string GetSigninToken()
        {
            return FormsAuthentication.FormsCookieName;
        }
        public static void SetAuth(User loginUser, HttpContextBase _curentHttpContext)
        {
            try
            {
                var user = new UserBaseModel
                {
                    Id = loginUser.Id,
                    FullName = loginUser.FullName,
                    Email = loginUser.Email,
                    Phone = loginUser.Phone
                };
                var loginToken = new FormsAuthenticationTicket(1, GetSigninToken(), DateTime.Now, DateTime.Now + FormsAuthentication.Timeout, FormsAuthentication.SlidingExpiration, JsonConvert.SerializeObject(user), FormsAuthentication.FormsCookiePath);
                SetAuthCookie(_curentHttpContext, loginToken, GetSigninToken());

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static void SetAuthCookie(HttpContextBase httpContext, FormsAuthenticationTicket authenticationTicket, string cookieName)
        {
            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            var cookie = new HttpCookie(cookieName, encryptedTicket);
            httpContext.Response.Cookies.Remove(cookie.Name);
            httpContext.Response.Cookies.Add(cookie);
        }
        public static UserBaseModel CurrentUser(HttpContextBase _curentHttpContext)
        {
            UserBaseModel user = null;
            var signinTokenCookie = _curentHttpContext.Request.Cookies[GetSigninToken()];
            if (signinTokenCookie != null && !string.IsNullOrEmpty(signinTokenCookie.Value))
            {
                try
                {
                    var token = FormsAuthentication.Decrypt(signinTokenCookie.Value);
                    user = JsonConvert.DeserializeObject<UserBaseModel>(token.UserData);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return user;
        }
        public static void LogOff(HttpContextBase httpContext)
        {
            var cookie = new HttpCookie(GetSigninToken());
            DateTime nowDateTime = DateTime.Now;
            cookie.Expires = nowDateTime.AddDays(-1);
            httpContext.Response.Cookies.Add(cookie);
            // Put user code to initialize the page here            
            FormsAuthentication.SignOut();
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            HttpContext.Current.Session.Abandon();
        }
    }
}