using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.User;
using ProjectX.WebApplication.IService;
using ProjectX.WebApplication.Service;

namespace ProjectX.WebApplication.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexClient()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var clientList =await _clientService.GetClientList(accessKey);
                return View(clientList);
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateClient()
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                TempData["UserName"] = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                return View();
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientAddRequest addClientRequest)
       {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var userName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));

                var user = await _clientService.AddClient(accessKey, addClientRequest);
                if (user == 200)
                {
                    TempData["AddedClient"] = "Client added successfully";
                }
                else
                {
                    TempData["Failed"] = "Failed";
                }
                return RedirectToAction("IndexClient");
            }
            else
            {
                return View("Login", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditClient(Guid clientID)
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accesskey != null)
            {
                ViewBag.UserName = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("UserName"));
                var client = await _clientService.GetClientById(accesskey, clientID);

                return View(client);
            }
            return RedirectToAction("IndexClient");
        }
        [HttpPost]
        public async Task<IActionResult> EditClient(ClientUpdateRequest updateClientRequest)
        {
            var accesskey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));

            if (accesskey != null)
            {
                var client = await _clientService.UpdateClient(accesskey, updateClientRequest);
                if (client == 200)
                {
                    TempData["EditedClient"] = "Client added successfully";
                }
                else
                {
                    TempData["FailedClient"] = "Failed";
                }
                return RedirectToAction("IndexClient");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteClient(Guid clientID)
        {
            var accessKey = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("SessionUser"));
            if (accessKey != null)
            {
                var user = await _clientService.DeleteClient(accessKey, clientID);
                if (user == 200)
                {
                    TempData["DeleteClient"] = "Client Deleted Successfully";
                }
                else
                {
                    TempData["FailedClient"] = "Client Deleted Failed";
                }
                return RedirectToAction("IndexClient");
            }
            else
            {
                return View("Login", "Home");
            }
        }
    }
}
