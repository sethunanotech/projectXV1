using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.Projects;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IDropdownService _dropdownService;
        public ProjectController(IProjectService projectService, IDropdownService dropdownService)
        {
            _projectService = projectService;
            _dropdownService = dropdownService;
        }
        [HttpGet]
        public async Task<IActionResult> IndexProject()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var projectList = await _projectService.GetProjectList(accessKey);
                return View(projectList);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateProject()
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
        public async Task<IActionResult> CreateProject(ProjectAddRequest addProjectRequest)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var user = await _projectService.AddProject(accessKey, addProjectRequest);
                if (user == 200)
                {
                    TempData["AddedProject"] = "Project added successfully";
                }
                else
                {
                    TempData["FailedProject"] = "Failed";
                }
                return RedirectToAction("IndexProject");
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProject(Guid projectId)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.client = await _dropdownService.GetClientDropdownList(accessKey);
                var project = await _projectService.GetProjectById(accessKey, projectId);
                return View(project);
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject(ProjectUpdateRequest updateProjectRequest)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));

                var project = await _projectService.UpdateProject(accessKey, updateProjectRequest);
                if (project == 200)
                {
                    TempData["EditedProject"] = "Project Updated Successfully";
                }
                else
                {
                    TempData["FailedProject"] = "Failed";
                }
                return RedirectToAction("IndexProject");
            }
            else
            {
                return View("Login", "Home");
            }
        }
       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var project = await _projectService.DeleteProject(accessKey, projectId);
                if (project == 200)
                {
                    TempData["DeleteProject"] = "Project Deleted Successfully";
                }
                else
                {
                    TempData["FailedProject"] = "Project Deleted Failed";
                }
                return RedirectToAction("IndexProject");
            }
            else
            {
                return View("Login", "Home");
            }
        }
    }
}
