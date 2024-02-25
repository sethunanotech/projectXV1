using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Controllers
{

    public class ProjectUserController : Controller
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IDropdownService _dropdownService;

        public ProjectUserController(IProjectUserService projectUserService, IDropdownService dropdownService)
        {
            _projectUserService = projectUserService;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> IndexProjectUser()
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accesskey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var clientList = await _projectUserService.GetProjectUserList(accesskey);
                return View(clientList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateProjectUser()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.project = await _dropdownService.GetProjectDropdownList(accessKey);
                ViewBag.User = await _dropdownService.GetUserDropDownList(accessKey);
               
                return View();
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProjectUser(ProjectUserAddRequest addProjectUserRequest)
        {

            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accesskey != null)
            {
                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
               
                var projectUser = await _projectUserService.AddProjectUser(accesskey, addProjectUserRequest);
                if (projectUser == 200)
                {
                    TempData["AddedProjectUser"] = " added successfully";
                }
                else
                {
                    TempData["FailedProjectUser"] = "Failed";
                }
                return RedirectToAction("IndexProjectUser");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public async Task<ActionResult> GetBindedUserName(Guid projectID)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                TempData["ProjectID"] = projectID;
                var value = Guid.Parse(projectID.ToString());
                ViewBag.User = await _dropdownService.GetBindedUserDropDownList(accessKey, value);
                return PartialView("UserDetails");
            }
            return RedirectToAction("CreateProjectUser");

        }
        [HttpGet]
        public async Task<IActionResult> EditProjectUser(Guid projectUserId)
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accesskey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.project = await _dropdownService.GetProjectDropdownList(accesskey);

                var projectUser = await _projectUserService.GetProjectUserById(accesskey, projectUserId);
                ViewBag.User = await _dropdownService.GetBindedUserDropDownList(accesskey, projectUser.ProjectID);
                return View(projectUser);
            }
            return RedirectToAction("IndexProjectUser");
        }
        [HttpPost]
        public async Task<IActionResult> EditProjectUser(ProjectUserUpdateRequest updateProjectUserRequest)
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accesskey != null)
            {

                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var projectUser = await _projectUserService.UpdateProjectUser(accesskey, updateProjectUserRequest);
                if (projectUser == 200)
                {
                    TempData["EditedProjectUser"] = " added successfully";
                }
                else
                {
                    TempData["FailedProjectUser"] = "Failed";
                }
                return RedirectToAction("IndexProjectUser");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProjectUser(Guid projectUserId)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var projectUser = await _projectUserService.DeleteProjectUser(accessKey, projectUserId);
                if (projectUser == 200)
                {
                    TempData["DeleteProjectUser"] = " Deleted Successfully";
                }
                else
                {
                    TempData["FailedProjectUser"] = " Deleted Failed";
                }
                return RedirectToAction("IndexProjectUser");
            }
            else
            {
                return View("Login", "Home");
            }

        }
    }

}