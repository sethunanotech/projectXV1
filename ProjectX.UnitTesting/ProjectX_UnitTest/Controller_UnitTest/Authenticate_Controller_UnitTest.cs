using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class Authenticate_Controller_UnitTest
    {
        #region Property
        public Mock<IAuthenticationService> mockAuthenticateService = new Mock<IAuthenticationService>();
        public Mock<ILogger<UserController>> mockLogger = new Mock<ILogger<UserController>>();
        private readonly UserLogin userLoginNotNull = AuthenticateMaster.UserLoginNotNull();
        private readonly UserLogin userLoginUserNameNull = AuthenticateMaster.UserLoginUserNameNull();
        private readonly UserLogin userLoginUserNameEmpty = AuthenticateMaster.UserLoginUserNameEmpty();
        private readonly UserLogin userLoginPasswordNull = AuthenticateMaster.UserLoginPasswordNull();
        private readonly UserLogin userLoginPasswordEmpty = AuthenticateMaster.UserLoginPasswordEmpty();
        private readonly Token tokenNotNull = AuthenticateMaster.TokenNotNull();
        private readonly ClientLogin clientLoginNotNull = AuthenticateMaster.ClientLoginNotNull();
        private readonly ClientLogin clientLoginClientIDEmpty = AuthenticateMaster.ClientLoginClientIDEmpty();
        private readonly ClientLogin clientLoginSecretCodeNull = AuthenticateMaster.ClientLoginSecretCodeNull();
        private readonly ClientLogin clientLoginSecretCodeEmpty = AuthenticateMaster.ClientLoginSecretCodeEmpty();
        private readonly Token tokenNull = null;
        private readonly User userNotNull=UserMaster.UserNotNull();
        private readonly User userNull=null;
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        private readonly Project projectSecretCodeNull = ProjectMaster.ProjectSecretCodeNull();
        private readonly Project projectNull = null;
        private readonly JwtSecurityToken jwtSecurityTokenNull = null;
        private readonly JwtSecurityToken jwtSecurityToken = AuthenticateMaster.GetJwtSecurityToken();
        private readonly TokenModel getTokenModel = AuthenticateMaster.GetTokenModel();
        private readonly TokenModel getTokenModelRefreshTokenNull = AuthenticateMaster.GetTokenModelRefreshTokenNull();
        private readonly RefreshTokenRequest refreshTokenRequestNotNull = AuthenticateMaster.RefreshTokenRequestNotNull();
        private readonly RefreshTokenRequest refreshTokenRequestAccessTokenNull = AuthenticateMaster.RefreshTokenRequestAccessTokenNull();
        private readonly RefreshTokenRequest refreshTokenRequestAccessTokenEmpty = AuthenticateMaster.RefreshTokenRequestAccessTokenEmpty();
        private readonly RefreshTokenRequest refreshTokenRequestRefreshTokenNull = AuthenticateMaster.RefreshTokenRequestRefreshTokenNull();
        private readonly RefreshTokenRequest refreshTokenRequestRefreshTokenEmpty = AuthenticateMaster.RefreshTokenRequestRefreshTokenEmpty();
        private readonly ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        private readonly ClaimsPrincipal claimsPrincipalNull = null;
      
        #endregion

        #region Login
        /// <summary>
        ///  requested userName is null
        /// </summary>
        [Fact]
        public void Login_UserNameNull_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginUserNameNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  requested userName is empty
        /// </summary>
        [Fact]
        public void Login_UserNameEmpty_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginUserNameEmpty);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  requested password is null
        /// </summary>
        [Fact]
        public void Login_PasswordNull_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginPasswordNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  requested password is empty
        /// </summary>
        [Fact]
        public void Login_PasswordEmpty_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginPasswordEmpty);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Invalid UserName and Password
        /// </summary>
        [Fact]
        public void Login_InvalidUser_ReturnsBadRequest()
        {
            //Assert
            mockAuthenticateService.Setup(p => p.IsUserExist(userLoginNotNull)).ReturnsAsync(userNull);

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Problem is genrating/Creating token
        /// </summary>
        [Fact]
        public void Login_ProblemInGeneratingToken_ReturnsBadRequest()
        {
            //Assert
            mockAuthenticateService.Setup(p => p.IsUserExist(userLoginNotNull)).ReturnsAsync(userNotNull);
            mockAuthenticateService.Setup(p=>p.CreateToken(userNotNull)).Returns(tokenNull);
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///   Token is created
        /// </summary>
        [Fact]
        public void Login_TokenIsCreated_ReturnsOkResult()
        {
            //Assert
            mockAuthenticateService.Setup(p => p.IsUserExist(userLoginNotNull)).ReturnsAsync(userNotNull);
            mockAuthenticateService.Setup(p => p.CreateToken(userNotNull)).Returns(tokenNotNull);
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.Login(userLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region GenerateToken

        /// <summary>
        ///  ClientID is Empty
        /// </summary>
        [Fact]
        public void GenerateToken_ClientIdEmpty_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginClientIDEmpty);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);

        }

        /// <summary>
        ///  ClientID is Null
        /// </summary>
        [Fact]
        public void GenerateToken_SecretCodeNull_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginSecretCodeNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  ClientID is Empty
        /// </summary>
        [Fact]
        public void GenerateToken_SecretCodeEmpty_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginSecretCodeEmpty);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Requested ClientID and SecretCode is Invalid
        /// </summary>
        [Fact]
        public void GenerateToken_InvalidRequest_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.IsProjectExit(clientLoginNotNull)).ReturnsAsync(projectNull);

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Response project SecretCode is Empty 
        /// </summary>
        [Fact]
        public void GenerateToken_ProjectsecreteCodeEmpty_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.IsProjectExit(clientLoginNotNull)).ReturnsAsync(projectSecretCodeNull);
            
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Generating the access token using JwtSecurityToken is null
        /// </summary>
        [Fact]
        public void GenerateToken_CreateNewTokenNull_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.IsProjectExit(clientLoginNotNull)).ReturnsAsync(projectNotNull);
            mockAuthenticateService.Setup(p => p.CreateNewToken(null, projectNotNull.SecretCode)).Returns(jwtSecurityTokenNull);

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Generating the new refesh token is Empty 
        /// </summary>
        [Fact]
        public void GenerateToken_GenerateNewRefreshTokenEmpty_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.IsProjectExit(clientLoginNotNull)).ReturnsAsync(projectNotNull);
            mockAuthenticateService.Setup(p => p.CreateNewToken(null, projectNotNull.SecretCode)).Returns(jwtSecurityToken);
            mockAuthenticateService.Setup(p => p.GenerateNewRefreshToken()).Returns("");

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Generating the new refesh token is Empty 
        /// </summary>
        [Fact]
        public void GenerateToken_RefreshTokenResponse_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.IsProjectExit(clientLoginNotNull)).ReturnsAsync(projectNotNull);
            mockAuthenticateService.Setup(p => p.CreateNewToken(null, projectNotNull.SecretCode)).Returns(jwtSecurityToken);
            mockAuthenticateService.Setup(p => p.GenerateNewRefreshToken()).Returns("v8keF/OxSWXPXA6deimN6MEAQQZMYgVwhHEgIuE4wy5tjCBBKGb+qwdWbIOwHd5toHLF5ROOBkgQa+l5NSckew==");
            mockAuthenticateService.Setup(p => p.RefreshTokenResponse()).Returns(getTokenModel);

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.GenerateToken(clientLoginNotNull);

            //Assert
            var result = authenticateResult.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region RefreshToken

        /// <summary>
        ///  Requested AccessToken is Empty
        /// </summary>
        [Fact]
        public void RefreshToken_AccessTokenEmpty_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestAccessTokenEmpty);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Requested AccessToken is Null
        /// </summary>
        [Fact]
        public void RefreshToken_AccessTokenNull_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestAccessTokenNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Requested RefreshToken is Empty
        /// </summary>
        [Fact]
        public void RefreshToken_RefreshTokenEmpty_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestRefreshTokenEmpty);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Requested RefreshToken is Null
        /// </summary>
        [Fact]
        public void RefreshToken_RefreshTokenNull_ReturnsBadRequest()
        {
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestRefreshTokenNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  GetPrincipalFromExpiredToken is Null
        /// </summary>
        [Fact]
        public void RefreshToken_GetPrincipalFromExpiredTokenNull_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.GetPrincipalFromExpiredToken(refreshTokenRequestNotNull.AccessToken)).Returns(claimsPrincipalNull);

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestNotNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  CreateNewToken is Null
        /// </summary>
        [Fact]
        public void RefreshToken_CreateNewTokenNull_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.GetPrincipalFromExpiredToken(refreshTokenRequestNotNull.AccessToken)).Returns(claimsPrincipal);
            mockAuthenticateService.Setup(p => p.CreateNewToken(claimsPrincipal.Claims.ToList(),"")).Returns(jwtSecurityTokenNull);
           
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestNotNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  GenerateNewRefreshToken is Empty
        /// </summary>
        [Fact]
        public void RefreshToken_GenerateNewRefreshTokenEmpty_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.GetPrincipalFromExpiredToken(refreshTokenRequestNotNull.AccessToken)).Returns(claimsPrincipal);
            mockAuthenticateService.Setup(p => p.CreateNewToken(claimsPrincipal.Claims.ToList(), "")).Returns(jwtSecurityToken);
            mockAuthenticateService.Setup(p => p.GenerateNewRefreshToken()).Returns("");

            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestNotNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        /// <summary>
        ///  Problem in generating token because generated refreshToken is Null 
        /// </summary>
        [Fact]
        public void RefreshToken_RefreshTokenResponseRefreshTokenNull_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.GetPrincipalFromExpiredToken(refreshTokenRequestNotNull.AccessToken)).Returns(claimsPrincipal);
            mockAuthenticateService.Setup(p => p.CreateNewToken(claimsPrincipal.Claims.ToList(), "")).Returns(jwtSecurityToken);
            mockAuthenticateService.Setup(p => p.GenerateNewRefreshToken()).Returns("v8keF/OxSWXPXA6deimN6MEAQQZMYgVwhHEgIuE4wy5tjCBBKGb+qwdWbIOwHd5toHLF5ROOBkgQa+l5NSckew==");
            mockAuthenticateService.Setup(p => p.RefreshTokenResponse()).Returns(getTokenModelRefreshTokenNull);
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestNotNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Problem in generating the accessToken is Null 
        /// </summary>
        [Fact]
        public void RefreshToken_RefreshTokenResponse_ReturnsBadRequest()
        {
            //Arrange
            mockAuthenticateService.Setup(p => p.GetPrincipalFromExpiredToken(refreshTokenRequestNotNull.AccessToken)).Returns(claimsPrincipal);
            mockAuthenticateService.Setup(p => p.CreateNewToken(claimsPrincipal.Claims.ToList(), "")).Returns(jwtSecurityToken);
            mockAuthenticateService.Setup(p => p.GenerateNewRefreshToken()).Returns("v8keF/OxSWXPXA6deimN6MEAQQZMYgVwhHEgIuE4wy5tjCBBKGb+qwdWbIOwHd5toHLF5ROOBkgQa+l5NSckew==");
            mockAuthenticateService.Setup(p => p.RefreshTokenResponse()).Returns(getTokenModel);
            //Act
            AuthenticateController authenticateController = new AuthenticateController(mockLogger.Object, mockAuthenticateService.Object);
            var authenticateResult = authenticateController.RefreshToken(refreshTokenRequestNotNull);

            //Assert
            var result = authenticateResult;
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion
    }
}
