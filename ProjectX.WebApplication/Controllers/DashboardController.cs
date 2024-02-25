using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectX.WebApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
            return View();
        }
    }
}
