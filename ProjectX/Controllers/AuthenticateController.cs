using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Login;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectX.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticateController(ILogger<UserController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.UserName))
            {
                _logger.LogError("UserName is Required");
                Serilog.Log.Error("Login: Error => UserName is Required");
                return BadRequest("UserName is Required");
            }
            if (string.IsNullOrEmpty(userLogin.Password))
            {
                _logger.LogError("Password is Required");
                Serilog.Log.Error("Login: Error => Password is Required");
                return BadRequest("Password is Required");
            }
            var userExist = await _authenticationService.IsUserExist(userLogin);
            if (userExist == null)
            {
                _logger.LogError("Invalid UserName and Password");
                Serilog.Log.Error("Login: Error => Invalid UserName and Password");
                return BadRequest("Invalid UserName and Password");
            }
            else
            {
                var token = _authenticationService.CreateToken(userExist);
                if (token == null)
                {
                    _logger.LogError("Problem in generating the token");
                    Serilog.Log.Error("Login: Error => Problem in generating the token");
                    return BadRequest("Problem in generating the token");
                }
                return Ok(token);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(ClientLogin clientLogin)
        {
            try
            {
                if (clientLogin.ClientID == Guid.Empty)
                {
                    _logger.LogError("ClientID is Required");
                    Serilog.Log.Error("GenerateToken: Error => ClientID is Required");
                    return BadRequest("ClientID is Required");
                }
                if (string.IsNullOrEmpty(clientLogin.SecretCode))
                {
                    _logger.LogError("SecretCode is Required");
                    Serilog.Log.Error("GenerateToken: Error => SecretCode is Required");
                    return BadRequest("SecretCode is Required");
                }
                var projectExist = await _authenticationService.IsProjectExit(clientLogin);
                if (projectExist == null)
                {
                    _logger.LogError("Invalid ClientID Or SecretCode");
                    Serilog.Log.Error("GenerateToken: Error => Invalid ClientID Or SecretCode");
                    return BadRequest("Invalid ClientID Or SecretCode");
                }
                if (string.IsNullOrEmpty(projectExist.SecretCode))
                {
                    _logger.LogError("Invalid ClientID Or SecretCode");
                    Serilog.Log.Error("GenerateToken: Error => Invalid ClientID Or SecretCode");
                    return BadRequest("Invalid ClientID Or SecretCode");
                }
                var token = _authenticationService.CreateNewToken(null, projectExist.SecretCode);
                if (token != null)
                {
                    var refreshToken = _authenticationService.GenerateNewRefreshToken();
                    if (string.IsNullOrEmpty(refreshToken))
                    {
                        _logger.LogError("RefreshToken is Required");
                        Serilog.Log.Error("GenerateToken: Error => RefreshToken is Required");
                        return BadRequest("RefreshToken is Required");
                    }
                    var refreshTokenResponse = _authenticationService.RefreshTokenResponse();
                    refreshTokenResponse.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(refreshTokenResponse);
                }
                else
                {
                    _logger.LogError("Token is Required");
                    Serilog.Log.Error("GenerateToken: Error => Token is Required");
                    return BadRequest("Token is Required");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Serilog.Log.Error("GenerateToken: Error =>" + ex.Message);
                return BadRequest("Invalid Client Request");
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(RefreshTokenRequest tokenRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(tokenRequest.AccessToken))
                {
                    _logger.LogError("AccessToken is Required");
                    Serilog.Log.Error("RefreshToken: Error => AccessToken is Required");
                    return BadRequest("AccessToken is Required");
                }
                if (string.IsNullOrEmpty(tokenRequest.RefreshToken))
                {
                    _logger.LogError("RefreshToken is Required");
                    Serilog.Log.Error("RefreshToken: Error => RefreshToken is Required");
                    return BadRequest("RefreshToken is Required");
                }
                var principal = _authenticationService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
                if (principal == null)
                {
                    _logger.LogError("Principal is Required");
                    Serilog.Log.Error("RefreshToken: Error => Principal is Required");
                    return BadRequest("Principal is Required");
                }
                var accessToken = _authenticationService.CreateNewToken(principal.Claims.ToList(), "");
                if (accessToken == null)
                {
                    _logger.LogError("NewAccessToken is required");
                    Serilog.Log.Error("RefreshToken: Error => NewAccessToken is required");
                    return BadRequest("NewAccessToken is required");
                }
                var refreshToken = _authenticationService.GenerateNewRefreshToken();
                if (string.IsNullOrEmpty(refreshToken))
                {
                    _logger.LogError("NewRefreshToken is required");
                    Serilog.Log.Error("RefreshToken: Error => NewRefreshToken is required");
                    return BadRequest("NewRefreshToken is required");
                }
                var refreshTokenResponse = _authenticationService.RefreshTokenResponse();
                refreshTokenResponse.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
                if (refreshTokenResponse.AccessToken == null || refreshTokenResponse.RefreshToken == null)
                {
                    _logger.LogError("Problem in generating AccessToken or RefreshToken");
                    Serilog.Log.Error("RefreshToken: Error => Problem in generating AccessToken or RefreshToken");
                    return BadRequest("Problem in generating AccessToken or RefreshToken");
                }
                return Ok(refreshTokenResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Serilog.Log.Error("RefreshToken: Error =>" + ex.Message);
                return BadRequest("Invalid Client Request");
            }
        }

    }
}
