using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectX.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace ProjectX.Infrastructure.Utility
{
    public class Cryptography : ICryptography
    {
        private readonly IConfiguration _configuration;
        public Cryptography(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            string accessToken = string.Empty;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HpnrSoitRoHucsleoNAlanGiDnsiac1lr6ie1ka2At8ivavn3iihtbyA"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier, user.UserName),
             new Claim(ClaimTypes.GivenName,user.Password ),
             new Claim(ClaimTypes.Role,"Admin" ),
            };
            var generateToken = new JwtSecurityToken("http://localhost:4200",
             "http://localhost:5000",
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);
            var generetedToken = new JwtSecurityTokenHandler().WriteToken(generateToken);
            accessToken = generetedToken;
            return accessToken;
        }
        public string HashThePassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var hasedPasswords = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hasedPasswords);
        }            
     
        public string Encrypt(string secretCode)
        {
            string secretkey= _configuration["Key:SecretKey"];
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] keys = Encoding.UTF8.GetBytes(secretkey);
            ICryptoTransform cryptoTransform = provider.CreateEncryptor(keys, keys);
            var memorystream = new MemoryStream();
            var cryptostream = new CryptoStream(memorystream, cryptoTransform, CryptoStreamMode.Write);
            byte[] input = Encoding.UTF8.GetBytes(secretCode);
            cryptostream.Write(input, 0, input.Length);
            cryptostream.FlushFinalBlock();
            return Convert.ToBase64String(memorystream.ToArray());
        }

        public async Task<string> SaveFile(IFormFile file, int version, string url)
        {
            string message = "";
            string fullPath;
            string databasePath;
            try
            {
                if (url != "")
                {
                    string oldFile = Path.Combine(Directory.GetCurrentDirectory(), url);
                    if (File.Exists(oldFile)) File.Delete(oldFile);
                }
                var folderName = Path.Combine("Resource");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                
                if (version == 0)
                {
                    fullPath = Path.Combine(pathToSave +"\\Thumbnails"+ "\\"+ file.FileName);
                    databasePath = Path.Combine(folderName + "\\Thumbnails" + "\\" + file.FileName);
                }
                else    
                {
                    fullPath = Path.Combine(pathToSave + "\\Package" + "\\" + version + "_" + file.FileName);
                    databasePath = Path.Combine(folderName + "\\Package" + "\\" + version + "_" + file.FileName);
                }
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                if (File.Exists(databasePath)) File.Delete(databasePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    message = databasePath;
                }
            }
            catch (Exception ex)
            {
                message = "";
            }
            return message;
        }
        public string GetExpiredToken()
        {
            string issuerSignKey = _configuration["Key:IssuerSigningKey"];
            return issuerSignKey;
        }
        public JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims, string authsignkey, string issuer, string audience, string validminutes)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authsignkey));
            _ = int.TryParse(validminutes, out int tokenValidityInMinutes);
            var token = new JwtSecurityToken(
                  issuer: issuer,
                  audience: audience,
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
