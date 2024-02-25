using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using ProjectX.UnitTesting.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class Authenticate_Service_UnitTest
    {
        #region Property
        public Mock<IMapper> mockMapper = new Mock<IMapper>();
        public Mock<ICryptography> mockCryptography = new Mock<ICryptography>();
        public Mock<IUser> mockUserRepository = new Mock<IUser>();
        public Mock<IProject> mockProjectRepository = new Mock<IProject>();
        public Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        public Mock<IAuthenticationService> mockAuthenticayteService = new Mock<IAuthenticationService>();
        private readonly UserLogin userLoginNotNull = AuthenticateMaster.UserLoginNotNull();
        private readonly Token tokenNotNull = AuthenticateMaster.TokenNotNull();
        private readonly User userNotNull = UserMaster.UserNotNull();
        private readonly User userNull = null;
        private readonly ClientLogin clientLoginNotNull = AuthenticateMaster.ClientLoginNotNull();
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        private readonly Project projectNull = null;
        private readonly JwtSecurityToken jwtSecurityTokenNull = null;
        private readonly JwtSecurityToken jwtSecurityToken = AuthenticateMaster.GetJwtSecurityToken();
        private readonly IEnumerable<Claim> claimsNull = null;
        private readonly List<Claim> authClaims = AuthenticateMaster.AuthClaims();
        private readonly TokenResponse tokenresponse = AuthenticateMaster.Gettokenresponse();
        private readonly TokenResponse tokenresponsenull = null;
        #endregion

        #region Login
        /// <summary>
        ///  returns the User Exist
        /// </summary>
        [Fact]
        public void Login_IsUserExist_ReturnsUsers()
        {
            // Arrange
            mockCryptography.Setup(p => p.HashThePassword(userLoginNotNull.Password)).Returns("6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=");
            mockMapper.Setup(m => m.Map<User>(userLoginNotNull)).Returns(userNotNull);
            mockUserRepository.Setup(p => p.IsUserNamePasswordExist(userNotNull)).ReturnsAsync(userNotNull);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.IsUserExist(userLoginNotNull);

            //Assert
            Assert.Equal(userNotNull, authenticateServiceResult.Result);
        }

        /// <summary>
        ///  returns the User Exist Null
        /// </summary>
        [Fact]
        public void Login_IsUserExist_ReturnsUsersNull()
        {
            // Arrange
            mockCryptography.Setup(p => p.HashThePassword(userLoginNotNull.Password)).Returns("6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=");
            mockMapper.Setup(m => m.Map<User>(userLoginNotNull)).Returns(userNotNull);
            mockUserRepository.Setup(p => p.IsUserNamePasswordExist(userNotNull)).ReturnsAsync(userNull);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.IsUserExist(userLoginNotNull);

            //Assert
            Assert.Equal(userNull, authenticateServiceResult.Result);
        }

        /// <summary>
        ///  returns the User Exist
        /// </summary>
        [Fact]
        public void Login_CreateToken_ReturnsToken()
        {
            // Arrange
           // mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "JwtSettings")]).Returns("test123");
           // mockConfiguration.SetupGet(x => x[It.Is<Expression<Func<T>>>()]).Returns("test123");
           // mockConfiguration.Setup(p => p["JwtSettings:TokenExpirationInMinutes"]).Returns("30");
            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.CreateToken(userNotNull);

            //Assert
            Assert.Equal(tokenNotNull.AccessToken, authenticateServiceResult.AccessToken);
        }

        /// <summary>
        ///  returns the User Exist Null
        /// </summary>
        [Fact]
        public void Login_CreateToken_ReturnsTokenNull()
        {
            // Arrange
            mockCryptography.Setup(p => p.HashThePassword(userLoginNotNull.Password)).Returns("6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=");
            mockMapper.Setup(m => m.Map<User>(userLoginNotNull)).Returns(userNotNull);
            mockUserRepository.Setup(p => p.IsUserNamePasswordExist(userNotNull)).ReturnsAsync(userNull);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.CreateToken(userNotNull);

            //Assert
           // Assert.Equal(userNull, authenticateServiceResult.AccessToken);
        }
        #endregion

        #region GenerateToken
        /// <summary>
        ///  Request is not exit in the project table
        /// </summary>
        [Fact]
        public void GenerateToken_IsProjectExist_ReturnsProjectNull()
        {
            // Arrange
             mockProjectRepository.Setup(p => p.CheckClientIdSecretCodeExist(clientLoginNotNull)).ReturnsAsync(projectNull);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.IsProjectExit(clientLoginNotNull);

            //Assert
            Assert.Equal(projectNull, authenticateServiceResult.Result);
        }

        /// <summary>
        ///  Request exit in the project table
        /// </summary>
        [Fact]
        public void GenerateToken_IsProjectExist_ReturnsProjects()
        {
            // Arrange
            mockProjectRepository.Setup(p => p.CheckClientIdSecretCodeExist(clientLoginNotNull)).ReturnsAsync(projectNotNull);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.IsProjectExit(clientLoginNotNull);

            //Assert
            Assert.Equal(projectNotNull, authenticateServiceResult.Result);
        }

        /// <summary>
        ///  Create New Token null
        /// </summary>
        [Fact]
        public void GenerateToken_CreateNewTokenNull_ReturnsJWtSecurityTokenNull()
        {
            // Arrange
            mockAuthenticayteService.Setup(p => p.GetTokenresponse()).Returns(tokenresponsenull);
            mockCryptography.Setup(p => p.CreateToken(claimsNull, tokenresponse.authSigningKey, tokenresponse.issuer, tokenresponse.audience, tokenresponse.validinminutes)).Returns(jwtSecurityTokenNull);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.CreateNewToken(null, projectNotNull.SecretCode);

            //Assert
            Assert.Equal(jwtSecurityTokenNull, authenticateServiceResult);
        }

        /// <summary>
        ///  Create New Token not null
        /// </summary>
        [Fact]
        public void GenerateToken_CreateNewTokenNotNull_ReturnsJWtSecurityTokenNull()
        {
            // Arrange
            mockAuthenticayteService.Setup(p => p.GetTokenresponse()).Returns(tokenresponse);
            mockCryptography.Setup(p => p.CreateToken(authClaims, tokenresponse.authSigningKey, tokenresponse.issuer, tokenresponse.audience, tokenresponse.validinminutes)).Returns(jwtSecurityToken);

            // Act
            AuthenticationService authenticateService = new AuthenticationService(mockMapper.Object, mockCryptography.Object, mockUserRepository.Object, mockProjectRepository.Object, mockConfiguration.Object);
            var authenticateServiceResult = authenticateService.CreateNewToken(null, projectNotNull.SecretCode);

            //Assert
            Assert.Equal(jwtSecurityToken, authenticateServiceResult);
        }
        #endregion  
    }
}
