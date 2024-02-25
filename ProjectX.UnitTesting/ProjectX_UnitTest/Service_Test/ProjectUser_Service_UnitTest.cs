using AutoMapper;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using ProjectX.UnitTesting.Model;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class ProjectUser_Service_UnitTest
    {

        #region Setup   
        public Mock<IProjectUser> mockProjectUserRepository = new Mock<IProjectUser>();
        public Mock<IClient> mockClientRepository = new Mock<IClient>();
        public Mock<IUser> mockUserRepository = new Mock<IUser>();
        public Mock<IProject> mockProjectRepository = new Mock<IProject>();
        public Mock<IMapper> mockMapper = new Mock<IMapper>();
        public Mock<ICryptography> mockCryptography = new Mock<ICryptography>();

        private readonly ProjectUser projectUserNull = null;
        private readonly GetUserResponse getUserResponseData = ProjectUserMaster.GetUserData();
        private readonly GetProjectResponse getProjectIsNotNull = ProjectMaster.ProjectIsNotNull();
        private readonly GetProjectResponse getProjectResponseIsNull = null;
        private readonly User UserNotNull = ProjectUserMaster.UserNotNull();
        private readonly User UserNull = null;
        private readonly GetUserResponse getUserResponsNull = null;
        private readonly ProjectUser projectUserNotNull = ProjectUserMaster.ProjectUserNotNull();
        private readonly List<ProjectUser> projectUserListNotNull = ProjectUserMaster.ProjectUserListNotNull();
        private readonly List<GetProjectUserResponse> getProjectUserListIsNull = null;
        private readonly List<GetProjectUserResponse> getProjectUserListIsNotNull = ProjectUserMaster.ListOfProjectUsers();
        private readonly GetProjectUserResponse getProjectUserIsNotNull = ProjectUserMaster.GetProjectUsers();
        private readonly GetProjectUserResponse getProjectUserIsNull = null;
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        private readonly Project projectNull = null;
        private readonly ProjectUserAddRequest addProjectUser = ProjectUserMaster.AddProjectUser();
        private readonly ProjectUserUpdateRequest updateProjectUser = ProjectUserMaster.UpdateProjectUser();
        #endregion

        #region GetProjectUser

        [Fact]
        public void Get_GetAll_ReturnProjectListNull()
        {
            // Arrange
            mockProjectUserRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(projectUserListNotNull);
            mockMapper.Setup(m => m.Map<List<GetProjectUserResponse>>(projectUserNotNull)).Returns(getProjectUserListIsNull);
            // Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.GetAll();

            //Assert
            Assert.Equal(getProjectUserListIsNull, projectUserResult.Result);
        }
        [Fact]
        public void Get_GetAll_ReturnProjectListNotNull()
        {
            // Arrange
            mockProjectUserRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(projectUserListNotNull);
            mockMapper.Setup(m => m.Map<List<GetProjectUserResponse>>(projectUserListNotNull)).Returns(getProjectUserListIsNotNull);
            // Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.GetAll();
            //Assert
            Assert.Equal(getProjectUserListIsNotNull, projectUserResult.Result);

        }
        #endregion


        #region GetProjectUserById

        [Fact]
        public void Get_GetByID_ReturnNotNull()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.GetByIdAsync(projectUserNotNull.Id)).ReturnsAsync(projectUserNotNull);
            mockMapper.Setup(m => m.Map<GetProjectUserResponse>(projectUserNotNull)).Returns(getProjectUserIsNotNull);
            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object); 
            var projectUserResult = projectUserService.GetByID(projectUserNotNull.Id);

            //Assert
            Assert.Equal(getProjectUserIsNotNull, projectUserResult.Result);
        }

        [Fact]
        public void Get_GetByID_ReturnProjectData()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.GetByIdAsync(projectUserNotNull.Id)).ReturnsAsync(projectUserNotNull);
            mockMapper.Setup(m => m.Map<GetProjectUserResponse>(projectUserNotNull)).Returns(getProjectUserIsNull);
            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.GetByID(projectUserNotNull.Id);

            //Assert
            Assert.Equal(getProjectUserIsNull, projectUserResult.Result);

        }
        #endregion

        #region PostProjectUser
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Post_AddedSuccess_ReturnProjectUser()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<ProjectUser>(addProjectUser)).Returns(projectUserNotNull);
            mockProjectUserRepository.Setup(p => p.AddAsync(projectUserNotNull)).ReturnsAsync(projectUserNotNull);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.AddProjectUser(addProjectUser).Result;

            //Assert
            Assert.Equal(projectUserNotNull, projectUserResult);

        }
        /// <summary>
        /// AddedFailed
        /// </summary>
        [Fact]
        public void Post_AddedFailed_ReturnProjectUser()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<ProjectUser>(addProjectUser)).Returns(projectUserNull);
            mockProjectUserRepository.Setup(p => p.AddAsync(projectUserNotNull)).ReturnsAsync(projectUserNull);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.AddProjectUser(addProjectUser).Result;

            //Assert
            Assert.Equal(projectUserNull, projectUserResult);

        }
        /// <summary>
        ///  Check ProjectId Exist 
        /// </summary>
        [Fact]
        public void Post_CheckProjectIdExist_ReturnProject()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(addProjectUser.ProjectID)).ReturnsAsync(projectNotNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNotNull)).Returns(getProjectIsNotNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(addProjectUser.ProjectID).Result;

            //Assert
            Assert.Equal(getProjectIsNotNull.ToString(), projectResult.ToString());
        }
        /// <summary>
        ///  Check ProjectId NotExist 
        /// </summary>
        [Fact]
        public void Post_CheckProjectIdNotExist_ReturnProjectAsNull()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(addProjectUser.ProjectID)).ReturnsAsync(projectNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNull)).Returns(getProjectResponseIsNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(addProjectUser.ProjectID);

            //Assert
            Assert.Equal(getProjectResponseIsNull, projectResult.Result);
        }

        /// <summary>
        ///  Check UserId Exist 
        /// </summary>
        [Fact]
        public void Post_CheckUserIdExist_ReturnUser()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(addProjectUser.UserID)).ReturnsAsync(UserNotNull);
            mockMapper.Setup(m => m.Map<GetUserResponse>(UserNotNull)).Returns(getUserResponseData);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetByID(addProjectUser.UserID).Result;

            //Assert
            Assert.Equal(getUserResponseData.ToString(), userResult.ToString());
        }
        /// <summary>
        ///  Check UserId NotExist 
        /// </summary>
        [Fact]
        public void Post_CheckUserIdNotExist_ReturnUserAsNull()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(addProjectUser.UserID)).ReturnsAsync(UserNull);
            mockMapper.Setup(m => m.Map<GetUserResponse>(UserNull)).Returns(getUserResponsNull);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetByID(addProjectUser.UserID).Result;

            //Assert
            Assert.Equal(getUserResponsNull, userResult);
        }

        /// <summary>
        ///  Check ProjectUserCombintion Exist 
        /// </summary>
        /// 
        [Fact]
        public void Post_CheckProjectUserCombinationExist_ReturnTrue()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.CheckCombinationExist(addProjectUser.UserID, addProjectUser.ProjectID)).ReturnsAsync(true);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID).Result;

            //Assert
            Assert.True(projectUserResult);
        }

        /// <summary>
        ///  Check ProjectUserCombintion Exist 
        /// </summary>
        /// 
        [Fact]
        public void Post_CheckProjectUserCombinationIsNotExist_ReturnFalse()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.CheckCombinationExist(addProjectUser.UserID, addProjectUser.ProjectID)).ReturnsAsync(false);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID).Result;

            //Assert
            Assert.False(projectUserResult);
        }
        #endregion

        #region Update

        /// <summary>
        ///  Check ProjectId Exist 
        /// </summary>
        [Fact]
        public void Put_CheckProjectIdExist_ReturnProject()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(addProjectUser.ProjectID)).ReturnsAsync(projectNotNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNotNull)).Returns(getProjectIsNotNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(addProjectUser.ProjectID).Result;

            //Assert
            Assert.Equal(getProjectIsNotNull.ToString(), projectResult.ToString());
        }
        /// <summary>
        ///  Check ProjectId NotExist 
        /// </summary>
        [Fact]
        public void Put_CheckProjectIdNotExist_ReturnProjectAsNull()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(addProjectUser.ProjectID)).ReturnsAsync(projectNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNull)).Returns(getProjectResponseIsNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(addProjectUser.ProjectID);

            //Assert
            Assert.Equal(getProjectResponseIsNull, projectResult.Result);
        }

        /// <summary>
        ///  Check UserId Exist 
        /// </summary>
        [Fact]
        public void Put_CheckUserIdExist_ReturnUser()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(addProjectUser.UserID)).ReturnsAsync(UserNotNull);
            mockMapper.Setup(m => m.Map<GetUserResponse>(UserNotNull)).Returns(getUserResponseData);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetByID(addProjectUser.UserID).Result;

            //Assert
            Assert.Equal(getUserResponseData.ToString(), userResult.ToString());
        }
        /// <summary>
        ///  Check UserId NotExist 
        /// </summary>
        [Fact]
        public void Put_CheckUserIdNotExist_ReturnUserAsNull()
        {
            //Arrange
            mockUserRepository.Setup(p => p.GetByIdAsync(addProjectUser.UserID)).ReturnsAsync(UserNull);
            mockMapper.Setup(m => m.Map<GetUserResponse>(UserNull)).Returns(getUserResponsNull);

            //Act
            UserService userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockCryptography.Object);
            var userResult = userService.GetByID(addProjectUser.UserID).Result;

            //Assert
            Assert.Equal(getUserResponsNull, userResult);
        }

        /// <summary>
        ///  Check ProjectUserCombintion Exist 
        /// </summary>
        /// 
        [Fact]
        public void Put_CheckProjectUserCombinationExist_ReturnTrue()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.CheckCombinationExist(addProjectUser.UserID, addProjectUser.ProjectID)).ReturnsAsync(true);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID).Result;

            //Assert
            Assert.True(projectUserResult);
        }

        /// <summary>
        ///  Check ProjectUserCombintion Exist 
        /// </summary>
        /// 
        [Fact]
        public void Put_CheckProjectUserCombinationIsNotExist_ReturnFalse()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.CheckCombinationExist(addProjectUser.UserID, addProjectUser.ProjectID)).ReturnsAsync(false);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID).Result;

            //Assert
            Assert.False(projectUserResult);
        }
        /// <summary>
        /// UpdateSuccess
        /// </summary>
        [Fact]
        public void Put_UpdateSuccess_ReturnProjectUser()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<ProjectUser>(updateProjectUser)).Returns(projectUserNotNull);
            mockProjectUserRepository.Setup(p => p.GetByIdAsync(updateProjectUser.Id)).ReturnsAsync(projectUserNotNull);
            mockProjectUserRepository.Setup(p => p.UpdateAsync(projectUserNotNull)).ReturnsAsync(projectUserNotNull);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.UpdateProjectUser(updateProjectUser).Result;

            //Assert
            Assert.Equal(projectUserNotNull, projectUserResult);

        }
        /// <summary>
        ///UpdateFailed
        /// </summary>
        [Fact]
        public void Put_UpdateFailed_ReturnProjectUser()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<ProjectUser>(addProjectUser)).Returns(projectUserNull);
            mockProjectUserRepository.Setup(p => p.UpdateAsync(projectUserNotNull)).ReturnsAsync(projectUserNull);

            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.UpdateProjectUser(updateProjectUser).Result;

            //Assert
            Assert.Equal(projectUserNull, projectUserResult);

        }
        #endregion  
        #region Delete

        /// <summary>
        /// DeletedFailed
        /// </summary>
        [Fact]
        public void Delete_RemoveProjectUser_ReturnDeletedFailed()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.GetByIdAsync(projectUserNotNull.Id)).ReturnsAsync(projectUserNull);
            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.RemoveProjectUser(projectUserNotNull.Id);
            //Assert
            Assert.Equal(projectUserNull, projectUserResult.Result);

        }
        /// <summary>
        /// DeletedSuccessfully
        /// </summary>
        [Fact]
        public void Delete_RemoveProjectUser_ReturnDeletedSuccessfully()
        {
            //Arrange
            mockProjectUserRepository.Setup(p => p.GetByIdAsync(projectUserNotNull.Id)).ReturnsAsync(projectUserNotNull);
            //Act
            ProjectUserService projectUserService = new ProjectUserService(mockProjectUserRepository.Object, mockMapper.Object);
            var projectUserResult = projectUserService.RemoveProjectUser(projectUserNotNull.Id);
            //Assert
            Assert.Equal(projectUserNotNull, projectUserResult.Result);

        }
        #endregion
    }
}
