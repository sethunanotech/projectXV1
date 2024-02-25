using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectX.Application.Service
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly ICryptography _cryptography;
        private readonly IUser _userRepository;
        private readonly IProject _projectRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IMapper mapper, ICryptography cryptography, IUser userRepository, IProject projectRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _cryptography = cryptography;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _configuration = configuration;
        }

        public async Task<User> IsUserExist(UserLogin userLogin)
        {
            var encrypt  = _cryptography.HashThePassword(userLogin.Password);
            var user = _mapper.Map<User>(userLogin);
            user.Password = encrypt;
            var userExist = await _userRepository.IsUserNamePasswordExist(user);
            return userExist;
        }
        public async Task<Project>IsProjectExit(ClientLogin clientLogin)
        {
            var projectExist = await _projectRepository.CheckClientIdSecretCodeExist(clientLogin);
            if(projectExist !=null)
            {
                return projectExist;
            }
            return null; 
        }

        public Token CreateToken(User user)
        {
            Token token = new Token();
            var expireTime = _configuration.GetSection("JwtSettings");
            TimeSpan time = TimeSpan.FromMinutes(Convert.ToInt32(expireTime["TokenExpirationInMinutes"]));
            var accesstoken = _cryptography.GenerateToken(user);
            token.AccessToken = accesstoken;
            token.TokenExpiresInMinutes = (int)time.TotalMinutes;
            return token;
        }
        public  ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters();
            var tokenValidation = _cryptography.GetExpiredToken();
            if (!string.IsNullOrEmpty(tokenValidation))
            {
                tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValidation)),
                    ValidateLifetime = false
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal =tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        public TokenResponse GetTokenresponse()
        {
            TokenResponse tokenresponses = new TokenResponse();
            tokenresponses.audience = _configuration["Key:ValidAudience"];
            tokenresponses.authSigningKey = _configuration["Key:IssuerSigningKey"];
            tokenresponses.issuer = _configuration["Key:ValidIssuer"];
            tokenresponses.validinminutes = _configuration["Key:TokenValidityInMinutes"];
            return tokenresponses;
        }
        public JwtSecurityToken CreateNewToken(IEnumerable<Claim> claims, string secretCode)
        {
            _ = new List<Claim>();
            if (claims != null)
            {
                _ = claims.ToList();
            }
            List<Claim> authClaims = new List<Claim>
                        {
                             new Claim(ClaimTypes.Name, secretCode),
                             new Claim(ClaimTypes.Role, "Client"),
                        };
           var tokenresponse = GetTokenresponse();
            if (tokenresponse != null)
            {
                if (tokenresponse.audience == null)
                {
                    return null;
                }
                if (tokenresponse.issuer == null)
                {
                    return null;
                }
                if (tokenresponse.validinminutes == null)
                {
                    return null;
                }
                if (tokenresponse.authSigningKey == null)
                {
                    return null;
                }
                else
                {
                    var token = _cryptography.CreateToken(authClaims, tokenresponse.authSigningKey, tokenresponse.issuer, tokenresponse.audience, tokenresponse.validinminutes);
                    return token;
                }
            }
            else
            {
                return null;
            }
        }
        public  string GenerateNewRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }         
        public TokenModel RefreshTokenResponse()
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.RefreshToken = GenerateNewRefreshToken();
            var expireTime = _configuration.GetSection("JwtSettings");
            TimeSpan time = TimeSpan.FromMinutes(Convert.ToInt32(expireTime["TokenExpirationInMinutes"]));
            tokenModel.TokenExpiresInMinutes= (int)time.TotalMinutes;
            return tokenModel;
        }
    }
}
