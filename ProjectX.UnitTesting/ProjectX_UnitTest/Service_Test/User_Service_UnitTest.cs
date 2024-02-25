using AutoMapper;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using ProjectX.UnitTesting.Model;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class User_Service_UnitTest
    {
        #region Property
        public Mock<IUser>mockUserRepository= new Mock<IUser>();
        public Mock<IClient>mockClientRepository= new Mock<IClient>();
        public Mock<IMapper>mockMapper= new Mock<IMapper>();
        public Mock<ICryptography>mockCryptography= new Mock<ICryptography>();
        private readonly List<GetUserResponse> GetUserResponselist = UserMaster.userListIsNotNull();
        private readonly List<GetUserResponse> GetUserResponseCountZero = new List<GetUserResponse>();
        private readonly List<User> UserListNotNull = UserMaster.UserListNotNull();
        private readonly List<User> UserListCountZero = new List<User>();
        private readonly User UserNotNull = UserMaster.UserNotNull();
        private readonly User? UserNull = null;
        private readonly Client ClientNotNull = UserMaster.ClientNotNull();
        private readonly GetUserResponse getUserResponse = UserMaster.GetUserListById();
        private readonly GetUserResponse? getUserResponseNull = null;
        private readonly UserUpdateRequest userUpdateRequestNotNull = UserMaster.UserUpdateRequest();
        private readonly UserAddRequest userAddRequestNotNull = UserMaster.UserAddRequestNotNull();
        private readonly string password = "Admin@123";
        private readonly GetClientResponse getClientResponse = ClientMaster.GetClientResponseNotNull();
        #endregion

        #region GetUserList
        /// <summary>
        /// Get UserList NotNull
        /// </summary>

        [Fact]
        public void Get_GetAll_ReturnUserList()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(UserListNotNull);
            mockMapper.Setup(m => m.Map<List<GetUserResponse>>(UserListNotNull)).Returns(GetUserResponselist);
            //Act
            UserService userService= new UserService(mockUserRepository.Object,mockMapper.Object,mockCryptography.Object);
            var userResult = userService.GetAll();
            //Assert
            Assert.Equal(GetUserResponselist, userResult.Result);
        }
        /// <summary>
        ///  Get User list null
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnUserListAsNull()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(UserListCountZero);
            mockMapper.Setup(m => m.Map<List<GetUserResponse>>(UserListNotNull)).Returns(GetUserResponseCountZero);
            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetAll();
            //Assert
            Assert.Equal(GetUserResponseCountZero, userResult.Result);
        }
        #endregion

        #region GetById
        /// <summary>
        /// Get UserData NotNull By Id
        /// </summary>
        [Fact]
        public void Get_GetUserById_ReturnUserData()
         {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(UserNotNull.Id)).ReturnsAsync(UserNotNull);
            mockMapper.Setup(m => m.Map<GetUserResponse>(UserNotNull)).Returns(getUserResponse);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetByID(UserNotNull.Id);
            //Assert
            Assert.Equal(getUserResponse, userResult.Result);
        }
        /// <summary>
        /// Get UserById No Records Found
        /// </summary>
        [Fact]
        public void Get_GetUserById_ReturnUserNull()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(UserNotNull.Id)).ReturnsAsync(UserNull);
            mockMapper.Setup(m => m.Map<GetUserResponse>(UserNotNull)).Returns(getUserResponseNull);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetByID(UserNotNull.Id);
            //Assert
            Assert.Equal(getUserResponseNull, userResult.Result);
        }
        #endregion

        #region Post
        /// <summary>
        /// User Added Successfully
        /// </summary>
        [Fact]
        public void Post_UserAddedSuccessfully_ReturnUserData()
        {
            //Arrange
            mockUserRepository.Setup(p => p.AddAsync(UserNotNull)).ReturnsAsync(UserNotNull);
            mockMapper.Setup(m => m.Map<User>(userAddRequestNotNull)).Returns(UserNotNull);
            mockCryptography.Setup(c => c.HashThePassword(UserNotNull.Password)).Returns(password);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.AddUser(userAddRequestNotNull);

            //Assert
            Assert.Equal(UserNotNull, userResult.Result);
        }
        /// <summary>
        /// User Added Failed
        /// </summary>
     
        [Fact]
        public void Post_UserAddedFailed_ReturnUserNull()
        {
            //Arrange
            mockUserRepository.Setup(p => p.AddAsync(UserNotNull)).ReturnsAsync(UserNull);
            mockMapper.Setup(m => m.Map<User>(userAddRequestNotNull)).Returns(UserNotNull);
            mockCryptography.Setup(c => c.HashThePassword(UserNotNull.Password)).Returns(password);
            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.AddUser(userAddRequestNotNull);

            //Assert
            Assert.Equal(UserNull, userResult.Result);
        }
       
        /// <summary>
        ///  Check ClientId Exist 
        /// </summary>
        [Fact]
        public void Post_CheckClientID_ReturnClientList()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(ClientNotNull.Id)).ReturnsAsync(ClientNotNull);
            mockMapper.Setup(m => m.Map<GetClientResponse>(ClientNotNull)).Returns(getClientResponse);
          
            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.GetByID(ClientNotNull.Id);

            //Assert
            Assert.Equal(getClientResponse, clientResult.Result);

        }
        #endregion

        #region Put
        /// <summary>
        /// User Updated Successfully
        /// </summary> 
        [Fact]
        public void Put_UserUpdateSuccessfully_ReturnUserData()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(ClientNotNull.Id)).ReturnsAsync(ClientNotNull);
            mockUserRepository.Setup(p => p.UpdateAsync(UserNotNull)).ReturnsAsync(UserNotNull);
            mockMapper.Setup(m => m.Map<User>(userUpdateRequestNotNull)).Returns(UserNotNull);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.UpdateUser(userUpdateRequestNotNull);
            //Assert
            Assert.Equal(UserNotNull, userResult.Result);

        }
        /// <summary>
        /// User Updated Failed
        /// </summary> 
        [Fact]
        public void Put_UserUpdatedFailed_ReturnUserNull()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(ClientNotNull.Id)).ReturnsAsync(ClientNotNull);
            mockUserRepository.Setup(p => p.UpdateAsync(UserNotNull)).ReturnsAsync(UserNull);
            mockMapper.Setup(m => m.Map<User>(userUpdateRequestNotNull)).Returns(UserNull);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.UpdateUser(userUpdateRequestNotNull);
            //Assert
            Assert.Equal(UserNull, userResult.Result);

        }
        #endregion

        #region Delete
        /// <summary>
        /// User Removed Successfully
        /// </summary>         
        [Fact]
        public void Delete_RemoveUser_DeletedSuccessfully()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(UserNotNull.Id)).ReturnsAsync(UserNotNull);
            mockUserRepository.Setup(p => p.RemoveByIdAsync(UserNotNull.Id));
            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.RemoveUser(UserNotNull.Id);

            //Assert
            Assert.Equal(UserNotNull, userResult.Result);
        }
        /// <summary>
        /// User Removed Failed
        /// </summary> 
        [Fact]
        public void Delete_RemoveUser_DeletedFailed()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(UserNotNull.Id)).ReturnsAsync(UserNull);
            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.RemoveUser(UserNotNull.Id);

            //Assert
            Assert.Equal(UserNull, userResult.Result);
        }
        #endregion
    }
}
