using Microsoft.IdentityModel.Tokens;
using ProjectX.Application.Usecases.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectX.UnitTesting.Model
{
    public class AuthenticateMaster
    {
        public static UserLogin UserLoginNotNull()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = "Admin";
            userLogin.Password = "Admin@123";
            return userLogin;
        }
        public static UserLogin UserLoginUserNameNull()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = null;
            userLogin.Password = "Admin@123";
            return userLogin;
        }
        public static UserLogin UserLoginUserNameEmpty()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = "";
            userLogin.Password = "Admin@123";
            return userLogin;
        }
        public static UserLogin UserLoginPasswordNull()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = "Admin";
            userLogin.Password = null;
            return userLogin;
        }
        public static UserLogin UserLoginPasswordEmpty()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = "Admin";
            userLogin.Password = "";
            return userLogin;
        }
        public static Token TokenNotNull()
        {
            Token token = new Token();
            token.AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiNkc5NHFLUEs4TFlOam5UbGxDcW0yRzNCVU0wOEF6T0s3eVczMHRmanJNYz0iLCJleHAiOjE3MDc4MTY0MDMsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.Gr7YImlBV_LYj11DjZqjelWFROqYMzv4Nm931X5CYzY";
            token.TokenExpiresInMinutes = 30;
            return token;
        }
        public static ClientLogin ClientLoginNotNull()
        {
            ClientLogin clientLogin = new ClientLogin();
            clientLogin.ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F");
            clientLogin.SecretCode = "5hvnNKv2Txs=";
            return clientLogin;
        }
        public static ClientLogin ClientLoginClientIDEmpty()
        {
            ClientLogin clientLogin = new ClientLogin();
            clientLogin.ClientID = Guid.Empty;
            clientLogin.SecretCode = "5hvnNKv2Txs=";
            return clientLogin;
        }
        public static ClientLogin ClientLoginSecretCodeNull()
        {
            ClientLogin clientLogin = new ClientLogin();
            clientLogin.ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F");
            clientLogin.SecretCode = null;
            return clientLogin;
        }
        public static ClientLogin ClientLoginSecretCodeEmpty()
        {
            ClientLogin clientLogin = new ClientLogin();
            clientLogin.ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F");
            clientLogin.SecretCode = "";
            return clientLogin;
        }
        public static JwtSecurityToken GetJwtSecurityToken()
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HpnrSoitRoHucsleoNAlanGiDnsiac1lr6ie1ka2At8ivavn3iihtbyA"));
            _ = int.TryParse("30", out int tokenValidityInMinutes);
            var token = new JwtSecurityToken(
                  issuer: "http://localhost:4200",
                  audience: "http://localhost:5000",
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public static TokenModel GetTokenModel()
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.RefreshToken = "xiIcM92ePpq/r5RAIbKAtFE2p8Yai6w2UCGtBgJHmlTFIQKEh6Y8IVBejohAnIYu6HA9XGueVNYurnaw7OTRbw==";
            tokenModel.TokenExpiresInMinutes=30;
            return tokenModel;
        }
        public static TokenModel GetTokenModelRefreshTokenNull()
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.RefreshToken =null;
            tokenModel.TokenExpiresInMinutes = 30;
            return tokenModel;
        }
        public static RefreshTokenRequest RefreshTokenRequestNotNull()
        {
            RefreshTokenRequest tokenModel = new RefreshTokenRequest();
            tokenModel.RefreshToken = "xiIcM92ePpq/r5RAIbKAtFE2p8Yai6w2UCGtBgJHmlTFIQKEh6Y8IVBejohAnIYu6HA9XGueVNYurnaw7OTRbw==";
            tokenModel.AccessToken= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MDc4ODYyMTIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.qc0RHD_dsnIJVtjmiccY7iAK6DHcFlsMLInGrVRbJ88";
            return tokenModel;
        }
        public static RefreshTokenRequest RefreshTokenRequestAccessTokenNull()
        {
            RefreshTokenRequest tokenModel = new RefreshTokenRequest();
            tokenModel.RefreshToken = "xiIcM92ePpq/r5RAIbKAtFE2p8Yai6w2UCGtBgJHmlTFIQKEh6Y8IVBejohAnIYu6HA9XGueVNYurnaw7OTRbw==";
            tokenModel.AccessToken = null;
            return tokenModel;
        }
        public static RefreshTokenRequest RefreshTokenRequestAccessTokenEmpty()
        {
            RefreshTokenRequest tokenModel = new RefreshTokenRequest();
            tokenModel.RefreshToken = "xiIcM92ePpq/r5RAIbKAtFE2p8Yai6w2UCGtBgJHmlTFIQKEh6Y8IVBejohAnIYu6HA9XGueVNYurnaw7OTRbw==";
            tokenModel.AccessToken = "";
            return tokenModel;
        }
        public static RefreshTokenRequest RefreshTokenRequestRefreshTokenNull()
        {
            RefreshTokenRequest tokenModel = new RefreshTokenRequest();
            tokenModel.RefreshToken = null;
            tokenModel.AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MDc4ODYyMTIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.qc0RHD_dsnIJVtjmiccY7iAK6DHcFlsMLInGrVRbJ88";
            return tokenModel;
        }
        public static RefreshTokenRequest RefreshTokenRequestRefreshTokenEmpty()
        {
            RefreshTokenRequest tokenModel = new RefreshTokenRequest();
            tokenModel.RefreshToken = "";
            tokenModel.AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MDc4ODYyMTIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.qc0RHD_dsnIJVtjmiccY7iAK6DHcFlsMLInGrVRbJ88";
            return tokenModel;
        }
        public static TokenResponse Gettokenresponse()
        {
            var Gettokenresponse = new TokenResponse()
            {
                authSigningKey = "HpnrSoitRoHucsleoNAlanGiDnsiac1lr6ie1ka2At8ivavn3iihtbyA",
                issuer = "http://localhost:4200",
                audience = "http://localhost:5000",
                validinminutes = "30"
            };
            return Gettokenresponse;
        }

        public static List<Claim> AuthClaims()
        {
            List<Claim> authClaims = new List<Claim>
                        {
                             new Claim(ClaimTypes.Name, "5hvnNKv2Txs=" ),
                        };
            return authClaims;
        }
    }
}
