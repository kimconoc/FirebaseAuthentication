using FirebasePhoneNumberAuthenticationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Diagnostics;

namespace FirebasePhoneNumberAuthenticationCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
                    //User user = new User();
                    //{
                    //    user.Phone = number;
                    //}
                    //Authenticator.SetAuth(user, HttpContext);
                }
            }
            catch (Exception ex)
            {

            }
            return default;
        }
    }
}