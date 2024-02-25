using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}/[action]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly ILogger<DropDownController> _logger;
        private readonly IDropDownService _dropDownService;
        public DropDownController(ILogger<DropDownController> logger, IDropDownService dropDownService)
        {
            _dropDownService = dropDownService;
            _logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetClientDropDown()
        {
            var clientValue = await _dropDownService.GetClientDropdownList();
            if (clientValue == null)
            {
                _logger.LogError("No records found");
                Serilog.Log.Error("GetClientDropDown: Error => No records found");
                return BadRequest("No records found");
            }
            return Ok(clientValue);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProjectDropDown()
        {
            var projectValue = await _dropDownService.GetProjectDropdownList();
            if (projectValue == null)
            {
                _logger.LogError("No records found");
                Serilog.Log.Error("GetProjectDropDown: Error => No records found");
                return BadRequest("No records found");
            }
            return Ok(projectValue);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBindedUserDropDown(Guid projectId)
        {
            var userValue = await _dropDownService.CheckProjectUserDropdown(projectId);
            if (userValue == null)
            {
                _logger.LogError("No records found");
                Serilog.Log.Error("GetBindedUserDropDown: Error => No records found");
                return BadRequest("No records found");
            }
            return Ok(userValue);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserDropDown()
        {
            var projectValue = await _dropDownService.GetUserDropdownList();
            if (projectValue == null)
            {
                _logger.LogError("No records found");
                Serilog.Log.Error("GetUserDropDown: Error => No records found");
                return BadRequest("No records found");
            }
            return Ok(projectValue);
        }
    }
}
