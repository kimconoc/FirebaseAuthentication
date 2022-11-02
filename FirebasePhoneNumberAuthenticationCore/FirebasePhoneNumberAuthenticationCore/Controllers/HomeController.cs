using FirebasePhoneNumberAuthenticationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Diagnostics;

namespace FirebasePhoneNumberAuthenticationCore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Test Remote Cookie
            //RemoteCurrentUserCookie()
            ViewBag.YourPhone = string.Empty;
            var user = GetCurrentUser();
            if(user != null)
            {
                ViewBag.YourPhone = user.Phone;
            }
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult ExecuteVerified(string number, string statusStr)
        {
            try
            {
                number = new JavaScriptSerializer().Deserialize<string>(number);
                bool status = new JavaScriptSerializer().Deserialize<bool>(statusStr);
                if (status)
                {
                    UserBaseModel userBaseModel = new UserBaseModel();
                    {
                        userBaseModel.Phone = number;
                    }
                    var user = GetCurrentUser();
                    if(user == null)
                        SetCurrentUserCookie(userBaseModel);
                }
            }
            catch (Exception ex)
            {

            }
            return View("Index");
        }
    }
}