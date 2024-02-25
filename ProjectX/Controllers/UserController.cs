using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.User;

namespace ProjectX.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/{v:apiVersion}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;
        public UserController(ILogger<UserController> logger, IUserService userService, IClientService clientService)
        {
            _logger = logger;
            _userService = userService;
            _clientService = clientService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usersList = await _userService.GetAll();
                if (usersList == null)
                {
                    _logger.LogError("No records found");
                    Serilog.Log.Error("Get: Error => No records found");
                    return NotFound();
                }
                return Ok(usersList);
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
                var userById = await _userService.GetByID(Id);
                if (userById == null)
                {
                    _logger.LogError("No records found for the given user ID");
                    Serilog.Log.Error("Get: Error => No records found for the given user ID");
                    return NotFound("No records found for the given user ID");
                }
                return Ok(userById);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Get: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, UserUpdateRequest updateUserRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Id != updateUserRequest.Id)
                    {
                        _logger.LogError("Invalid User ID");
                        Serilog.Log.Error("Put: Error => Invalid User ID");
                        return BadRequest("Invalid User ID");
                    }
                    var clientExit = await _clientService.GetByID(updateUserRequest.ClientId);
                    if (clientExit == null)
                    {
                        _logger.LogError("Invalid Client ID");
                        Serilog.Log.Error("Put: Error => Invalid Client ID");
                        return BadRequest("Invalid Client ID");
                    }
                    else
                    {
                        var user = await _userService.UpdateUser(updateUserRequest);
                        if (user == null)
                        {
                            _logger.LogError("User Updated Failed");
                            Serilog.Log.Error("Post: Error => User Updated Failed");
                            return BadRequest("User Updated Failed");
                        }
                        return NoContent();
                    }
                }
                else
                {
                    _logger.LogError("Invalid User");
                    Serilog.Log.Error("Put: Error => Invalid User");
                    return BadRequest("Invalid User");
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
        public async Task<IActionResult> Post(UserAddRequest addUserRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var clientExit = await _clientService.GetByID(addUserRequest.ClientID);
                    if (clientExit != null)
                    {
                        var userData = await _userService.AddUser(addUserRequest);
                        if (userData == null)
                        {
                            _logger.LogError("User Added Failed");
                            Serilog.Log.Error("Post: Error => User Added Failed");
                            return BadRequest("User Added Failed");
                        }
                        return CreatedAtAction("Get", new { Id = userData.Id }, userData);
                    }
                    else
                    {
                        _logger.LogError("Invalid ClientID");
                        Serilog.Log.Error("Post: Error => Invalid ClientID");
                        return BadRequest("Invalid ClientID");
                    }
                }
                else
                {
                    _logger.LogError("Invalid User");
                    Serilog.Log.Error("Post: Error => Invalid User");
                    return BadRequest("Invalid User");
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
                var user = await _userService.RemoveUser(Id);
                if (user == null)
                {
                    _logger.LogError("No records found for the given user ID");
                    Serilog.Log.Error("Delete: Error => No records found for the given user ID");
                    return NotFound("No records found for the given user ID");
                }
                return Ok(user);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Delete: Error => " + ex.Message);
                return BadRequest("Invalid client request");
            }

        }
    }
}
