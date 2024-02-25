using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.Login;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;
        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var accessToken = _loginService.GenerateToken(userLogin);
                if (accessToken.AccessToken == null)
                {
                    TempData["Token"] = JsonConvert.SerializeObject(accessToken.AccessToken);
                    return RedirectToAction("Login", "Home");
                }
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(accessToken.AccessToken));
                HttpContext.Session.SetString("UserName", JsonConvert.SerializeObject(userLogin.UserName));
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
