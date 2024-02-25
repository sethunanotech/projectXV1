using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectX.Application.Service
{
    public interface IAuthenticationService
    {
        Task<User> IsUserExist(UserLogin userLogin);
        Token CreateToken(User user);
        Task<Project> IsProjectExit(ClientLogin clientLogin);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        TokenResponse GetTokenresponse();
        JwtSecurityToken CreateNewToken(IEnumerable<Claim> claims, string secretCode);
        string GenerateNewRefreshToken();
        TokenModel RefreshTokenResponse();
    }
}
