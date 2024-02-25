using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.ProjectUsers;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}")]
    [ApiController]
    public class ProjectUserController : ControllerBase
    {

        private readonly ILogger<ProjectUserController> _logger;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IProjectUserService _service;

        public ProjectUserController(ILogger<ProjectUserController> logger, IProjectService projectService, IUserService userService, IProjectUserService service)
        {
            _logger = logger;
            _projectService = projectService;
            _userService = userService;
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var projectUserList = await _service.GetAll();
                if (projectUserList == null)
                {
                    _logger.LogError("No records found");
                    Serilog.Log.Error("Get: Error => No records found");
                    return NotFound();
                }
                return Ok(projectUserList);
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
                var getProjectUser = await _service.GetByID(Id);
                if (getProjectUser == null)
                {
                    _logger.LogError("No records found for the given projectUser ID");
                    Serilog.Log.Error("Get: Error => No records found for the given projectUser ID");
                    return NotFound("No records found for the given projectUser ID");
                }
                return Ok(getProjectUser);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, ProjectUserUpdateRequest updateProjectUser)
        {
            try
            {
                if (Id != updateProjectUser.Id)
                {
                    _logger.LogError("Invalid ProjectUser ID");
                    Serilog.Log.Error("Put: Error => Invalid ProjectUser ID");
                    return BadRequest("Invalid ProjectUser ID");
                }
                if (ModelState.IsValid)
                {
                    var userExist = await _userService.GetByID(updateProjectUser.UserID);
                    var projectExist = await _projectService.GetByID(updateProjectUser.ProjectID);
                    if (userExist == null)
                    {
                        _logger.LogError("Invalid UserID");
                        Serilog.Log.Error("Put: Error => Invalid UserID");
                        return BadRequest("Invalid UserID");
                    }
                    else if (projectExist == null)
                    {
                        _logger.LogError("Invalid projectID");
                        Serilog.Log.Error("Put: Error => Invalid projectID");
                        return BadRequest("Invalid projectID");
                    }
                    else
                    {
                        var isUserProjectExist = await _service.CheckCombination(updateProjectUser.UserID, updateProjectUser.ProjectID);
                        if (!isUserProjectExist)
                        {
                            _logger.LogError("Invalid projectID and userID");
                            Serilog.Log.Error("Post: Error => Invalid projectID and userID");
                            return BadRequest("Invalid projectID and userID");
                        }
                        var projectUser = await _service.UpdateProjectUser(updateProjectUser);
                        if (projectUser != null)
                        {
                            return NoContent();
                        }
                        _logger.LogError("ProjectUser Update Failed");
                        Serilog.Log.Error("Post: Error => ProjectUser Update Failed");
                        return BadRequest("ProjectUser Update Failed");
                    }
                }
                else
                {
                    _logger.LogError("Invalid ProjectUser");
                    Serilog.Log.Error("Put: Error => Invalid ProjectUser");
                    return BadRequest("Invalid ProjectUser");
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
        public async Task<IActionResult> Post(ProjectUserAddRequest addProjectUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userExist = await _userService.GetByID(addProjectUser.UserID);
                    var projectExist = await _projectService.GetByID(addProjectUser.ProjectID);
                    if (userExist == null)
                    {
                        _logger.LogError("Invalid UserID");
                        Serilog.Log.Error("Post: Error => Invalid UserID");
                        return BadRequest("Invalid User ID");
                    }
                    else if (projectExist == null)
                    {
                        _logger.LogError("Invalid ProjectID");
                        Serilog.Log.Error("Post: Error => Invalid ProjectID");
                        return BadRequest("Invalid Project ID");
                    }
                    else
                    {
                        var isUserProjectExist = await _service.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID);
                        if (!isUserProjectExist)
                        {
                            _logger.LogError("Invalid projectID and userID");
                            Serilog.Log.Error("Post: Error => Invalid projectID and userID");
                            return BadRequest("Invalid projectID and userID");
                        }
                        var projectUser = await _service.AddProjectUser(addProjectUser);
                        if (projectUser != null)
                        {

                            return CreatedAtAction("Get", new { Id = projectUser.Id }, projectUser);
                        }
                        _logger.LogError("ProjectUser Added Failed");
                        Serilog.Log.Error("Post: Error => ProjectUser Added Failed");
                        return BadRequest("ProjectUser Added Failed");
                    }
                }
                else
                {
                    _logger.LogError("Invalid ProjectUser");
                    Serilog.Log.Error("Post: Error => Invalid ProjectUser");
                    return BadRequest("Invalid ProjectUser");
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

                var projectUser = await _service.RemoveProjectUser(Id);
                if (projectUser == null)
                {
                    _logger.LogError("No records found for the given projectUser ID");
                    Serilog.Log.Error("Delete: Error => No records found for the given projectUser ID");
                    return NotFound("No records found for the given projectUser ID");
                }
                return Ok(projectUser);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Delete: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }
    }
}
