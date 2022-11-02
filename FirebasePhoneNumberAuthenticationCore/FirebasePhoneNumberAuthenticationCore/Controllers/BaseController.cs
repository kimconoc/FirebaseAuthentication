using FirebasePhoneNumberAuthenticationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FirebasePhoneNumberAuthenticationCore.Controllers
{
    public class BaseController : Controller
    {
        private readonly string _keyCurrentUser = "CurrentUser";
        protected void SetCurrentUserCookie(UserBaseModel userData)
        {
            var userDataJson = JsonConvert.SerializeObject(userData);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(10)
            };
            Response.Cookies.Append(_keyCurrentUser, userDataJson, options);
        }
        protected UserBaseModel GetCurrentUser()
        {
            UserBaseModel user = null;
            try
            {
                var cookieValue = Request.Cookies[_keyCurrentUser];
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    user = JsonConvert.DeserializeObject<UserBaseModel>(cookieValue);
                }
            }
            catch
            {
                throw;
            }
            return user;
        }
        protected void RemoteCurrentUserCookie()
        {
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append(_keyCurrentUser, value, options);
        }
    }
}
