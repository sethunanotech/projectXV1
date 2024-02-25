
using Microsoft.AspNetCore.Http;
using ProjectX.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static ProjectX.Infrastructure.Utility.Cryptography;

namespace ProjectX.Infrastructure.Utility
{
    public interface ICryptography
    {
        string GenerateToken(User user);
        string HashThePassword(string password);
        string Encrypt(string secretCode);
        Task<string> SaveFile(IFormFile file, int version,string url);
        string GetExpiredToken();
        JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims, string authsignkey, string issuer, string audience, string validminutes);
    }
}