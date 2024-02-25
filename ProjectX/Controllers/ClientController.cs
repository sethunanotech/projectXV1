using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;
        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var getClientList = await _clientService.GetAll();
                if (getClientList == null)
                {
                    _logger.LogError("No records found");
                    Serilog.Log.Error("Get: Error => No records found");
                    return NotFound("No records found");
                }
                return Ok(getClientList);
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
                var getClients = await _clientService.GetByID(Id);
                if (getClients == null)
                {
                    _logger.LogError("No records found for the given client ID");
                    Serilog.Log.Error("Get: Error => No records found for the given client ID");
                    return NotFound("No records found for the given client ID");
                }
                return Ok(getClients);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, ClientUpdateRequest updateClient)
        {
            try
            {
                if (Id != updateClient.Id)
                {
                    _logger.LogError("Invalid Client ID");
                    Serilog.Log.Error("Put: Error => Invalid Client ID");
                    return BadRequest("Invalid Client ID");
                }
                if (ModelState.IsValid)
                {
                    var updatedClient = await _clientService.UpdateClient(updateClient);
                    if (updatedClient == null)
                    {
                        _logger.LogError("Client Updated Failed");
                        Serilog.Log.Error("Post: Error => Client Updated Failed");
                        return BadRequest("Client Updated Failed");
                    }
                    return NoContent();
                }
                else
                {
                    _logger.LogError("Invalid Client");
                    Serilog.Log.Error("Put: Error => Invalid Client");
                    return BadRequest("Invalid Client");
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
        public async Task<IActionResult> Post(ClientAddRequest addClient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var addedClient = await _clientService.AddClient(addClient);
                    if (addedClient == null)
                    {
                        _logger.LogError("Client Added Failed");
                        Serilog.Log.Error("Post: Error => Client Added Failed");
                        return BadRequest("Client Added Failed");
                    }
                    return CreatedAtAction("Get", new { Id = addedClient.Id }, addedClient);
                }
                else
                {
                    _logger.LogError("Invalid client");
                    Serilog.Log.Error("Post: Error => Invalid Client");
                    return BadRequest("Invalid client");
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

                var client = await _clientService.RemoveClient(Id);
                if (client == null)
                {
                    _logger.LogError("No records found for the given client ID");
                    Serilog.Log.Error("Delete: Error => No records found for the given client ID");
                    return NotFound("No records found for the given client ID");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Delete: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }

    }
}
