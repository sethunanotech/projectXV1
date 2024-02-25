using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Package;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ILogger<PackageController> _logger;
        private readonly IPackageService _packageService;
        private readonly IProjectService _projectService;
        private readonly IEntityService _entityService;
        public PackageController(ILogger<PackageController> logger, IPackageService packageService, IProjectService projectService, IEntityService entityService)
        {
            _logger = logger;
            _packageService = packageService;
            _projectService = projectService;
            _entityService = entityService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listOfPackages = await _packageService.GetAll();

                if (!listOfPackages.Any())
                {
                    _logger.LogError("No records found");
                    Serilog.Log.Error("Get: Error => No records found");
                    return NotFound();
                }
                return Ok(listOfPackages);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                var package = await _packageService.GetByID(Id);
                if (package == null)
                {
                    _logger.LogError("No records found for the given package ID");
                    Serilog.Log.Error("Get: Error => No records found for the given package ID");
                    return NotFound("No records found for the given package ID");
                }
                return Ok(package);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, PackageUpdateRequest updatePackageRequest)
        {
            try
            {
                if (Id != updatePackageRequest.Id)
                {
                    _logger.LogError("Invalid Package ID");
                    Serilog.Log.Error("Put: Error => Invalid Package ID");
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    var projectExist = await _projectService.GetByID(updatePackageRequest.ProjectID);
                    if (projectExist == null)
                    {
                        _logger.LogError("Invalid Project ID");
                        Serilog.Log.Error("Post: Error => Invalid Project ID");
                        return BadRequest("Invalid Project ID");
                    }
                    var entityExist = await _entityService.GetByID(updatePackageRequest.EntityID);
                    if (entityExist == null)
                    {
                        _logger.LogError("Invalid Entity ID");
                        Serilog.Log.Error("Post: Error => Invalid Entity ID");
                        return BadRequest("Invalid Entity ID");
                    }
                    var isProjectEntityExist = await _packageService.CheckCombinationprojectEntityExist(updatePackageRequest.EntityID, updatePackageRequest.ProjectID);
                    if (!isProjectEntityExist)
                    {
                        _logger.LogError("Invalid projectID and EntityID");
                        Serilog.Log.Error("Post: Error => Invalid projectID and EntityID");
                        return BadRequest("Invalid projectID and EntityID");
                    }
                    var package = await _packageService.UpdatePackage(updatePackageRequest);
                    if (package != null)
                    {
                        return NoContent();
                    }
                    _logger.LogError("Package Updated Failded");
                    Serilog.Log.Error("Post: Error => Package Updated Failed");
                    return BadRequest("Package Updated Failded");
                }
                else
                {
                    _logger.LogError("Invalid Package");
                    Serilog.Log.Error("Put: Error => Invalid Package");
                    return BadRequest("Invalid Package");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Put: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(PackageAddRequest addPackageRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var projectExist = await _projectService.GetByID(addPackageRequest.ProjectID);
                    if (projectExist == null)
                    {
                        _logger.LogError("Invalid Project ID");
                        Serilog.Log.Error("Post: Error => Invalid Project ID");
                        return BadRequest("Invalid Project ID");
                    }
                    var entityExist = await _entityService.GetByID(addPackageRequest.EntityID);
                    if (entityExist == null)
                    {
                        _logger.LogError("Invalid Entity ID");
                        Serilog.Log.Error("Post: Error => Invalid Entity ID");
                        return BadRequest("Invalid Entity ID");
                    }
                    var isProjectEntityExist = await _packageService.CheckCombinationprojectEntityExist(addPackageRequest.EntityID, addPackageRequest.ProjectID);
                    if (!isProjectEntityExist)
                    {
                        _logger.LogError("Invalid projectID and EntityID");
                        Serilog.Log.Error("Post: Error => Invalid projectID and EntityID");
                        return BadRequest("Invalid projectID and EntityID");
                    }
                    var package = await _packageService.AddPackage(addPackageRequest);
                    if (package != null)
                    {
                        return CreatedAtAction("Get", new { Id = package.Id }, package);
                    }
                    _logger.LogError("Package Added Failded");
                    Serilog.Log.Error("Post: Error => Package Added Failed");
                    return BadRequest("Package Added Faild");
                }
                else
                {
                    _logger.LogError("Invalid Package");
                    Serilog.Log.Error("Post: Error => Invalid Package");
                    return BadRequest("Invalid Package");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Post: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var package = await _packageService.RemovePackage(Id);
                if (package != null)
                {
                    return Ok(package);
                }
                _logger.LogError("No records found for the given package ID");
                Serilog.Log.Error("Delete: Error => No records found for the given package ID");
                return NotFound("No records found for the given package ID");
            }
            catch (Exception ex)
            {

                Serilog.Log.Error("Delete: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }
    }
}
