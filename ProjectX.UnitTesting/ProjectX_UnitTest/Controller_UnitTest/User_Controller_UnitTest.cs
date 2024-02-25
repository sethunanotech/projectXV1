using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class User_Controller_UnitTest
    {
        #region Property
        public Mock<IUserService> mockUserService = new Mock<IUserService>();
        public Mock<IClientService> mockClientService = new Mock<IClientService>();
        public Mock<ILogger<UserController>> mockLogger = new Mock<ILogger<UserController>>();
        private readonly List<GetUserResponse> userlistNotNull = UserMaster.userListIsNotNull();
        private readonly List<GetUserResponse>? userlistNull = null;
        private readonly GetUserResponse getUserResponse = UserMaster.GetUserListById();
        private readonly GetUserResponse? getUserResponseNull = null;
        private readonly Client ClientNotNull = UserMaster.ClientNotNull();
        private readonly Client? ClientNull = null;
        private readonly UserAddRequest userAddRequestNotNull = UserMaster.UserAddRequestNotNull();
        private readonly User UserNotNull = UserMaster.UserNotNull();
        private readonly User? UserNull = null;
        private readonly UserUpdateRequest userUpdateRequestNotNull = UserMaster.UserUpdateRequest();
        private readonly GetClientResponse getClientResponse = ClientMaster.GetClientResponseNotNull();
        private readonly GetClientResponse getClientResponseNull = null;
        #endregion

        #region GetUser
        /// <summary>
        /// Return User List as NotNull
        /// </summary>
        [Fact]
        public void Get_ReturnGetUserResponseList_ReturnsOK()
        {
            //Arrange 
            mockUserService.Setup(u=>u.GetAll()).ReturnsAsync(userlistNotNull);
            //Act
            UserController userController = new UserController(mockLogger.Object,mockUserService.Object,mockClientService.Object);
            var userList = userController.Get();
            //Assert 
            var result= userList.Result as OkObjectResult;
            Assert.Equal(userlistNotNull, result?.Value);
        }
        /// <summary>
        /// No Records Found
        /// </summary>
        [Fact]
        public void Get_GetUserResponseListIsNull_ReturnsNotFound()
        {
            //Arrange 
            mockUserService.Setup(u => u.GetAll()).ReturnsAsync(userlistNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var userList = userController.Get();
            //Assert 
            var result = userList.Result;
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region GetUserById
        /// <summary>
        /// Return User Data By Id
        /// </summary>
        [Fact]
        public void Get_GetUserResponseById_ReturnOk()
        {
            //Arrange           
            mockUserService.Setup(p => p.GetByID(UserNotNull.Id)).ReturnsAsync(getUserResponse);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var userList = userController.Get(UserNotNull.Id);
            //Assert 
            var result = userList.Result as OkObjectResult;
            Assert.Equal(getUserResponse, result?.Value);
        }
        /// <summary>
        /// Invalid User Id
        /// </summary>
        [Fact]
        public void Get_InvalidUserId_ReturnNotFound()
        {
            //Arrange           
            mockUserService.Setup(p => p.GetByID(getUserResponse.Id)).ReturnsAsync(getUserResponseNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var userList = userController.Get(getUserResponse.Id);
            //Assert 
            var result = userList.Result;
            Assert.IsType<NotFoundObjectResult>(result);  
        }
        #endregion

        #region Add User
        /// <summary>
        /// User Added Successfully
        /// </summary>
        [Fact]
        public void Post_CheckClientIdExist_ReturnCreatedAtAction()
        {
            //Arrange
            mockClientService.Setup(u => u.GetByID(ClientNotNull.Id)).ReturnsAsync(getClientResponse);
            mockUserService.Setup(u => u.AddUser(userAddRequestNotNull)).ReturnsAsync(UserNotNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var addUserData = userController.Post(userAddRequestNotNull);
            //Assert
            var result = addUserData.Result;
            Assert.IsType<CreatedAtActionResult>(result);
        }
        /// <summary>
        /// Invalid ClientId
        /// </summary>
        [Fact]
        public void Post_CheckClientIdExist_ReturnBadRequest()
        {
            //Arrange
            mockClientService.Setup(u => u.GetByID(ClientNotNull.Id)).ReturnsAsync(getClientResponseNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var addUserData = userController.Post(userAddRequestNotNull);
            //Assert
            var result = addUserData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        /// <summary>
        /// User Added Failed
        /// </summary>
        [Fact]
        public void Post_CheckIfUserAdded_ReturnBadRequest()
        {
            //Arrange
            mockClientService.Setup(u => u.GetByID(ClientNotNull.Id)).ReturnsAsync(getClientResponse);
            mockUserService.Setup(u => u.AddUser(userAddRequestNotNull)).ReturnsAsync(UserNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var addUserData = userController.Post(userAddRequestNotNull);
            //Assert
            var result = addUserData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Update User
        /// <summary>
        /// Requested Id Is NotEqual to Params Id
        /// </summary>
        [Fact]
        public void Put_IdIsEqual_ReturnBadRequest()
        {
            //Arrange 
            var userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            //Act
            var result = userController.Put(default,userUpdateRequestNotNull);
            //Assert
            var updateUser = result.Result;
            Assert.IsType<BadRequestObjectResult>(updateUser);
        }
        /// <summary>
        /// User Updated Successfully
        /// </summary>
        [Fact]
        public void Put_CheckClientExist_ReturnNoContent()
        { 
            //Arrange
            mockClientService.Setup(u => u.GetByID(ClientNotNull.Id)).ReturnsAsync(getClientResponse);
            mockUserService.Setup(u => u.UpdateUser(userUpdateRequestNotNull)).ReturnsAsync(UserNotNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var updateUserData = userController.Put(userUpdateRequestNotNull.Id,userUpdateRequestNotNull);
            //Assert
            var result = updateUserData.Result;
            Assert.IsType<NoContentResult>(result);
        }
        /// <summary>
        /// Invalid Client Id
        /// </summary>
        [Fact]
        public void Put_CheckClientIdExist_ReturnBadRequestObjectResult()
        {
            //Arrange
            mockClientService.Setup(u => u.GetByID(ClientNotNull.Id)).ReturnsAsync(getClientResponse);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var updateUserData = userController.Put(userUpdateRequestNotNull.Id, userUpdateRequestNotNull);
            //Assert
            var result = updateUserData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        /// <summary>
        /// User Updated Failed
        /// </summary>
        [Fact]
        public void Put_CheckUserNull_ReturnBadRequestObjectResult()
        {
            //Arrange
            mockClientService.Setup(u => u.GetByID(ClientNotNull.Id)).ReturnsAsync(getClientResponse);
            mockUserService.Setup(u => u.UpdateUser(userUpdateRequestNotNull)).ReturnsAsync(UserNull);
            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var updateUserData = userController.Put(userUpdateRequestNotNull.Id,userUpdateRequestNotNull);
            //Assert
            var result = updateUserData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// User Deleted Successfully
        /// </summary>
        [Fact]
        public void Delete_RemoveUserById_ReturnsOk()
        {
            //Arrange
            mockUserService.Setup(p => p.RemoveUser(userUpdateRequestNotNull.Id)).ReturnsAsync(UserNotNull);

            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var DeleteUserData = userController.Delete(userUpdateRequestNotNull.Id);

            //Assert
            var result = (DeleteUserData.Result);
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// User Deleted failed
        /// </summary>
        [Fact]
        public void Delete_InvalidUserId_ReturnNotFoundObjectResult()
        {
            //Arrange
            mockUserService.Setup(p => p.RemoveUser(UserNotNull.Id)).ReturnsAsync(UserNull);

            //Act
            UserController userController = new UserController(mockLogger.Object, mockUserService.Object, mockClientService.Object);
            var DeleteUserData = userController.Delete(UserNotNull.Id);

            //Assert
            var result = DeleteUserData.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
