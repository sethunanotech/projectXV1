using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.Entity;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Controllers
{
    public class EntityController : Controller
    {
        private readonly IEntityService _entityService;
        private readonly IDropdownService _dropdownService;
        public EntityController(IEntityService entityService, IDropdownService dropdownService)
        {
           _entityService = entityService;
           _dropdownService = dropdownService;
        }
        public async Task<IActionResult> IndexEntity()
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accesskey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var entityList = await _entityService.GetEntityList(accesskey);
                return View(entityList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateEntity()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.project = await _dropdownService.GetProjectDropdownList(accessKey);
                return View();
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateEntity(EntityAddRequest entityAddRequest, IFormFile imageFile)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
  
                var entity = await _entityService.AddEntity(accessKey, entityAddRequest,imageFile);

                if (entity == 200)
                {
                    TempData["AddedEntity"] = "";
                }
                else
                {
                    TempData["FailedEntity"] = "Failed";
                }
                return RedirectToAction("IndexEntity");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEntity(Guid entityID)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.project = await _dropdownService.GetProjectDropdownList(accessKey);
                var pack = await _entityService.GetEntityById(accessKey, entityID);
                return View(pack);
            }
            return RedirectToAction("IndexEntity");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEntity(EntityUpdateRequest entityUpdateRequest)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accessKey != null)
            {
                var entity = await _entityService.UpdateEntity(accessKey, entityUpdateRequest);
                if (entity == 200)
                {
                    TempData["EditedEntity"] = "Entity added successfully";
                }
                else
                {
                    TempData["FailedEntity"] = "Failed";
                }
                return RedirectToAction("IndexEntity");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEntity(Guid entityID)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var package = await _entityService.DeleteEntity(accessKey, entityID);
                if (package == 200)
                {
                    TempData["DeleteEntity"] = "Entity Deleted Successfully";
                }
                else
                {
                    TempData["FailedEntity"] = "Entity Deleted Failed";
                }
                return RedirectToAction("IndexEntity");
            }
            else
            {
                return View("Login", "Home");
            }
        }

    }
}
