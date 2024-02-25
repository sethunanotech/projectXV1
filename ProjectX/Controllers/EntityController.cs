using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Entity;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly ILogger<EntityController> _logger;
        private readonly IProjectService _projectService;
        private readonly IEntityService _entityService;
        public EntityController(ILogger<EntityController> logger, IProjectService projectService,IEntityService entityService)
        {
            _logger = logger;
            _projectService = projectService;
            _entityService = entityService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listOfEntity = await _entityService.GetAll();
                if (!listOfEntity.Any())
                {
                    _logger.LogError("No records found");
                    Serilog.Log.Error("Get: Error => No records found");
                    return NotFound();
                }
                return Ok(listOfEntity);               
            }
            catch(Exception ex)
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
                var entity = await _entityService.GetByID(Id);
                if (entity == null)
                {
                    _logger.LogError("No records found for the given entity ID");
                    Serilog.Log.Error("Get: Error => No records found for the given entity ID");
                    return NotFound("No records found for the given entity ID");
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(EntityAddRequest addEntityRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var projectExist = await _projectService.GetByID(addEntityRequest.ProjectID);
                    if (projectExist == null)
                    {
                        _logger.LogError("Invalid Project ID");
                        Serilog.Log.Error("Post: Error => Invalid Project ID");
                        return BadRequest("Invalid Project ID");
                    }
                    var entity = await _entityService.AddEntity(addEntityRequest);
                    if(entity != null)
                    {
                        return CreatedAtAction("Get", new { entity.Id }, entity);                                             
                    }
                    _logger.LogError("Entity Added Failed");
                    Serilog.Log.Error("Post: Error => Entity Added Failed");
                    return BadRequest("Entity Added Failed");
                }
                else
                {
                    _logger.LogError("Invalid Entity");
                    Serilog.Log.Error("Post: Error => Invalid Entity");
                    return BadRequest("Invalid Entity");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Post: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }


        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, EntityUpdateRequest entityUpdateRequest)
        {
            try
            {
                if (Id != entityUpdateRequest.Id)
                {
                    _logger.LogError("Invalid Entity ID");
                    Serilog.Log.Error("Put: Error => Invalid Entity ID");
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    var projectExist = await _projectService.GetByID(entityUpdateRequest.ProjectID);
                    if (projectExist == null)
                    {
                        _logger.LogError("Invalid Project ID");
                        Serilog.Log.Error("Post: Error => Invalid Project ID");
                        return BadRequest("Invalid Project ID");
                    }
                    var entity = await _entityService.UpdateEntity(entityUpdateRequest);
                    if (entity != null)
                    {
                        return NoContent();
                    }
                    _logger.LogError("Entity Updated Failded");
                    Serilog.Log.Error("Post: Error => Entity Updated Failed");
                    return BadRequest("Entity Updated Failded");
                }
                else
                {
                    _logger.LogError("Invalid Entity");
                    Serilog.Log.Error("Put: Error => Invalid Entity");
                    return BadRequest("Invalid Entity");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Put: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var entity = await _entityService.RemoveEntity(Id);
                if (entity != null)
                {
                    return Ok(entity);
                }
                _logger.LogError("No records found for the given entity ID");
                Serilog.Log.Error("Delete: Error => No records found for the given entity ID");
                return NotFound("No records found for the given entity ID");
            }
            catch (Exception ex)
            {

                Serilog.Log.Error("Delete: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }
    }
}
