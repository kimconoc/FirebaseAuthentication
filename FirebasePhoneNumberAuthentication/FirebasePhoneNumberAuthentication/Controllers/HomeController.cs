using FirebasePhoneNumberAuthentication.MemCached;
using FirebasePhoneNumberAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FirebasePhoneNumberAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult ExecuteVerified(string number,string statusStr)
        {
            try
            {
                number = new JavaScriptSerializer().Deserialize<string>(number);
                bool status = new JavaScriptSerializer().Deserialize<bool>(statusStr);
                if(status)
                {
                    User user = new User();
                    {
                        user.Phone = number;
                    }
                    Authenticator.SetAuth(user, HttpContext);
                }    
            }
            catch (Exception ex)
            {
                
            }
            return default;
        }
    }
}