using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Projects;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IClientService _clientService;
        private readonly IProjectService _projectService;
        public ProjectController(ILogger<ProjectController> logger, IClientService clientService, IProjectService projectService)
        {
            _logger = logger;
            _clientService = clientService;
            _projectService = projectService;

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var getProjectsList = await _projectService.GetAll();
                if (getProjectsList == null)
                {
                    _logger.LogError("No records found");
                    Serilog.Log.Error("Get: Error => No records found");
                    return NotFound("No records foun");
                }
                return Ok(getProjectsList);
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
                var getProjects = await _projectService.GetByID(Id);
                if (getProjects == null)
                {
                    _logger.LogError("No records found for the given project ID");
                    Serilog.Log.Error("Get: Error => No records found for the given project ID");
                    return NotFound("No records found for the given project ID");
                }
                return Ok(getProjects);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, ProjectUpdateRequest updateProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Id != updateProject.Id)
                    {
                        _logger.LogError("Invalid Project ID");
                        Serilog.Log.Error("Put: Error => Invalid Project ID");
                        return BadRequest("Invalid Project Id");
                    }
                    var clientExist = await _clientService.GetByID(updateProject.ClientID);
                    if (clientExist == null)
                    {
                        _logger.LogError("Invalid ClientID");
                        Serilog.Log.Error("Put: Error => Invalid ClientID");
                        return BadRequest("Invalid ClientID");
                    }
                    var projects = await _projectService.UpdateProject(updateProject);
                    if (projects == null)
                    {
                        _logger.LogError("Project Updated Failed");
                        Serilog.Log.Error("Post: Error => Project Updated Failed");
                        return BadRequest("Project Updated Failed");
                    }

                    return NoContent();
                }
                else
                {
                    _logger.LogError("Invalid Project");
                    Serilog.Log.Error("Post: Error =>Invalid Project");
                    return BadRequest("Invalid Project");
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
        public async Task<IActionResult> Post(ProjectAddRequest addProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var clientExist = await _clientService.GetByID(addProject.ClientID);
                    if (clientExist == null)
                    {
                        _logger.LogError("Invalid ClientID");
                        Serilog.Log.Error("Put: Error => Invalid ClientID");
                        return BadRequest("Invalid ClientID");
                    }
                    var projectData = await _projectService.AddProject(addProject);
                    if (projectData == null)
                    {
                        _logger.LogError("Project Added Failed");
                        Serilog.Log.Error("Post: Error => Project Added Failed");
                        return BadRequest("Project Added Failed");
                    }
                    return CreatedAtAction("Get", new { Id = projectData.Id }, projectData);
                }
                else
                {
                    _logger.LogError("Invalid Project");
                    Serilog.Log.Error("Post: Error => Invalid Project");
                    return BadRequest();
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
                var projects = await _projectService.RemoveProject(Id);
                if (projects == null)
                {
                    _logger.LogError("No records found for the given project ID");
                    Serilog.Log.Error("Delete: Error => No records found for the given project ID");
                    return NotFound("No records found for the given project ID");
                }
                return Ok(projects);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Delete: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }
    }
}
