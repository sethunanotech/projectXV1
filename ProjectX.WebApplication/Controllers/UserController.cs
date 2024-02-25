using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.User;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDropdownService _dropdownService;
        public UserController(IUserService userService, IDropdownService dropdownService)
        {
            _userService = userService;
            _dropdownService = dropdownService;
        }
        [HttpGet]
        public async Task<IActionResult> IndexUser()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var userList = await _userService.GetUserList(accessKey);
                return View(userList);
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                TempData["UserName"] = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));

                ViewBag.client = await _dropdownService.GetClientDropdownList(accessKey);
                return View();
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserAddRequest addUserRequest)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
           
                var user = await _userService.AddUser(accessKey, addUserRequest);
                if (user == 200)
                {
                    TempData["AddedSuccessfully"] = "User added successfully";
                }
                else
                {
                    TempData["Failed"] = "Failed";
                }
                return RedirectToAction("IndexUser");
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(Guid userId)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.client = await _dropdownService.GetClientDropdownList(accessKey);
                var user = await _userService.GetUserById(accessKey, userId);
                return View(user);
            }
            else
            {
                return View("Login", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest updateUserRequest)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
             
                var user = await _userService.UpdateUser(accessKey, updateUserRequest);
                if (user == 200)
                {
                    TempData["EditedSuccessfully"] = "User Updated Successfully";
                }
                else
                {
                    TempData["Failed"] = "Failed";
                }
                return RedirectToAction("IndexUser");
            }
            else
            {
                return View("Login", "Home");
            }
        }
       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUsers(Guid UserId)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var user = await _userService.DeleteUser(accessKey, UserId);
                if (user == 200)
                {
                    TempData["Delete"] = "User Deleted Successfully";
                }
                else
                {
                    TempData["Failed"] = "User Deleted Failed";
                }
                return RedirectToAction("IndexUser");
            }
            else
            {
                return View("Login", "Home");
            }
        }
    }
}
