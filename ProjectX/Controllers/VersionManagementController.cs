using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.VersionManagement;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}/[action]")]
    [ApiController]
    public class VersionManagementController : ControllerBase
    {
        private readonly ILogger<VersionManagementController> _logger;
        private readonly IVersionManagementService _versionManagementService;
        public VersionManagementController(ILogger<VersionManagementController> logger, IVersionManagementService versionManagementService)
        {
            _versionManagementService = versionManagementService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetUpdates(GetUpdatesRequest getUpdatesRequest)
        {
            try
            {
                if (getUpdatesRequest.ProjectId == Guid.Empty)
                {
                    _logger.LogError("Get: GetUpdates => Invalid ProjectID");
                    Serilog.Log.Error("Get: GetUpdates => Invalid ProjectID");
                    return BadRequest("Invalid ProjectID");
                }
                if (getUpdatesRequest.Version <= 0)
                {
                    _logger.LogError("Get: GetUpdates => Invalid Version");
                    Serilog.Log.Error("Get: GetUpdates => Invalid Version");
                    return BadRequest("Invalid Version");
                }
                var updatesList = await _versionManagementService.GetUpdatedPackageList(getUpdatesRequest);
                if (updatesList.Count > 0)
                {
                    return Ok(updatesList);
                }
                else
                {
                    _logger.LogError("Get: GetUpdates => No updates found for this project Id");
                    Serilog.Log.Error("Get: GetUpdates => No updates found for this project Id");
                    return NotFound("No updates found for this project Id");
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error("Get: GetUpdates => " + e.Message);
                return BadRequest("Invalid client request");
            }

        }
    }
}
