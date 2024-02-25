using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.Package;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly IDropdownService _dropdownService;
        public PackageController(IPackageService packageService, IDropdownService dropdownService)
        {
            _packageService = packageService;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> IndexPackage()
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accesskey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var packageList = await _packageService.GetPackageList(accesskey);
                return View(packageList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreatePackage()
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
        public async Task<IActionResult> CreatePackage(PackageAddRequest addPackageRequest)
        {

            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accessKey != null)
            {
                var package = await _packageService.AddPackage(accessKey, addPackageRequest);
                if (package == 200)
                {
                    TempData["AddedPackage"] = "Package added successfully";
                }
                else
                {
                    TempData["FailedPackage"] = "Failed";
                }
                return RedirectToAction("IndexPackage");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePackage(Guid packageID)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                ViewBag.project = await _dropdownService.GetProjectDropdownList(accessKey);
                var pack = await _packageService.GetPackageById(accessKey, packageID);
                return View(pack);
            }
            return RedirectToAction("IndexPackage");
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePackage(PackageUpdateRequest updatePackageRequest)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accessKey != null)
            {
                var package = await _packageService.UpdatePackage(accessKey, updatePackageRequest);
                if (package == 200)
                {
                    TempData["EditedPackage"] = "Package added successfully";
                }
                else
                {
                    TempData["FailedPackage"] = "Failed";
                }
                return RedirectToAction("IndexPackage");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeletePackage(Guid packageID)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var package = await _packageService.DeletePackage(accessKey, packageID);
                if (package == 200)
                {
                    TempData["DeletePackage"] = "Package Deleted Successfully";
                }
                else
                {
                    TempData["FailedPackage"] = "Package Deleted Failed";
                }
                return RedirectToAction("IndexPackage");
            }
            else
            {
                return View("Login", "Home");
            }
        }
    }
}
