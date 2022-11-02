using FirebasePhoneNumberAuthenticationCore.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FirebasePhoneNumberAuthenticationCore.MemCached
{
    public class Authenticator
    {
        public static string GetSigninToken()
        {
            return "Go_stay";
        }
        public static void SetAuth(HttpResponse response,User user)
        {
            try
            {
                var userData = new UserBaseModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone
                };
                SetAuthCookie(response, JsonConvert.SerializeObject(user));
            }
            catch
            {
                throw;
            }

        }
        private static void SetAuthCookie(HttpResponse response, string userData)
        {
            string key = GetSigninToken();
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            response.Cookies.Append(key, userData, options);
        }
        public static UserBaseModel CurrentUser(HttpRequest request)
        {
            string cookieKey = GetSigninToken();
            UserBaseModel user = null;
            try
            {
                var cookieValue = request.Cookies[cookieKey];
                if(!string.IsNullOrEmpty(cookieValue))
                {
                    user = JsonConvert.DeserializeObject<UserBaseModel>(cookieKey);
                }    
            }
            catch
            {
                throw;
            }
            return user;
        }
        public static void LogOff(HttpResponse response)
        {
            string cookieKey = GetSigninToken();
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            response.Cookies.Append(cookieKey, value, options);
        }
    }
}
