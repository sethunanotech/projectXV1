using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class ProjectUser_Controller_UnitTest
    {
        public Mock<IProjectUserService> mockProjectUserService = new Mock<IProjectUserService>();
        public Mock<IUserService> mockUserService = new Mock<IUserService>();
        public Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
        public Mock<ILogger<ProjectUserController>> mockLogger = new Mock<ILogger<ProjectUserController>>();
        private readonly List<GetProjectUserResponse> projectUserListIsNull = null;
        private readonly List<GetProjectUserResponse> projectUserListIsNotNull = ProjectUserMaster.ListOfProjectUsers();
        private readonly GetProjectUserResponse getProjectUserIsNull = null;
        private readonly ProjectUser projectUserNotNull = ProjectUserMaster.ProjectUserNotNull();
        private readonly ProjectUser projectUserNull = null;
        private readonly GetProjectUserResponse getProjectUserNotNull = ProjectUserMaster.GetProjectUsers();
        private readonly ProjectUserAddRequest addProjectUser = ProjectUserMaster.AddProjectUser(); 
        private readonly GetUserResponse getUserResponseData = ProjectUserMaster.GetUserData();
        private readonly GetProjectResponse getProjectIsNotNull = ProjectMaster.ProjectIsNotNull();
        private readonly ProjectUserUpdateRequest updateProjectUser = ProjectUserMaster.UpdateProjectUser();

        #region GetProjectUser 

        /// <summary>
        /// GetProjectUserListIsNull
        /// </summary>
        [Fact]
        public void GetProjectUser_GetProjectUserListIsNull_ReturnNotFound()
        {
            //Arrange
            mockProjectUserService.Setup(p => p.GetAll()).ReturnsAsync(projectUserListIsNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Get();

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<NotFoundResult>(projectUserResult);
        }
        /// <summary>
        /// GetProjectUserListIsNotNull
        /// </summary>
        [Fact]
        public void GetProjectUser_GetProjectUserListIsNotNull_ReturnOk()
        {
            //Arrange
            mockProjectUserService.Setup(p => p.GetAll()).ReturnsAsync(projectUserListIsNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Get();

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<OkObjectResult>(projectUserResult);
        }
        #endregion
        #region GetProjectUserById

        /// <summary>
        /// GetProjectUserByIdIsNull
        /// </summary>
        [Fact]
        public void GetProjectUser_GetProjectUserByIdIsNull_ReturnNotFound()
        {
            //Arrange
            mockProjectUserService.Setup(p => p.GetByID(projectUserNotNull.Id)).ReturnsAsync(getProjectUserIsNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Get(projectUserNotNull.Id);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<NotFoundObjectResult>(projectUserResult);
        }
        /// <summary>
        /// GetProjectByIdIsNotNull
        /// </summary>
        [Fact]
        public void GetProjectUser_GetProjectByIdIsNotNull_ReturnOk()
        {
            //Arrange
            mockProjectUserService.Setup(p => p.GetByID(projectUserNotNull.Id)).ReturnsAsync(getProjectUserNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Get(projectUserNotNull.Id);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<OkObjectResult>(projectUserResult);
        }
        #endregion


        #region AddProjectUser

        /// <summary>
        /// UserIsNull
        /// </summary>

        [Fact]
        public void AddProjectUser_UserIsNull_ReturnsBadRequest()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetByID(addProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Post(addProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// ProjectIsNull
        /// </summary>
        [Fact]
        public void AddProjectUser_ProjectIsNull_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(addProjectUser.UserID)).ReturnsAsync(getUserResponseData);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Post(addProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// CheckCombinationFalse
        /// </summary>
        [Fact]
        public void AddProjectUser_CheckCombinationFalse_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(addProjectUser.UserID)).ReturnsAsync(getUserResponseData);
            mockProjectService.Setup(p => p.GetByID(addProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);
            mockProjectUserService.Setup(p => p.CheckCombination(addProjectUser.UserID,addProjectUser.ProjectID)).ReturnsAsync(false);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Post(addProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// AddProjectUserFailed
        /// </summary>
        [Fact]
        public void AddProjectUser_AddProjectUserFailed_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(addProjectUser.UserID)).ReturnsAsync(getUserResponseData);
            mockProjectService.Setup(p => p.GetByID(addProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);
            mockProjectUserService.Setup(p => p.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID)).ReturnsAsync(true);
            mockProjectUserService.Setup(p => p.AddProjectUser(addProjectUser)).ReturnsAsync(projectUserNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Post(addProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<CreatedAtActionResult>(projectUserResult);
        }
        /// <summary>
        /// AddProjectUserSuccessfull
        /// </summary>
        [Fact]
        public void AddProjectUser_AddProjectUserSuccessfull_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(addProjectUser.UserID)).ReturnsAsync(getUserResponseData);
            mockProjectService.Setup(p => p.GetByID(addProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);
            mockProjectUserService.Setup(p => p.CheckCombination(addProjectUser.UserID, addProjectUser.ProjectID)).ReturnsAsync(true);
            mockProjectUserService.Setup(p => p.AddProjectUser(addProjectUser)).ReturnsAsync(projectUserNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Post(addProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        #endregion


        #region UpdateProjectUser

        /// <summary>
        /// InvalidProjectUserId
        /// </summary>

        [Fact]
        public void UpdateProjectUser_InvalidProjectUserId_ReturnsBadRequest()
        {
          
            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Put(new Guid("60D0347F-312A-43C3-AB60-08DC0C2703A7"),updateProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// UserIsNull
        /// </summary>
        [Fact]
        public void UpdateProjectUser_UserIsNull_ReturnsBadRequest()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetByID(updateProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Put(new Guid("793804B3-3A7C-40E4-DE67-08DC119A7DBE"), updateProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// ProjectIsNull
        /// </summary>
        [Fact]
        public void UpdateProjectUser_ProjectIsNull_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(updateProjectUser.UserID)).ReturnsAsync(getUserResponseData);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Put(new Guid("793804B3-3A7C-40E4-DE67-08DC119A7DBE"), updateProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// CheckCombinationFalse
        /// </summary>
        [Fact]
        public void UpdateProjectUser_CheckCombinationFalse_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(updateProjectUser.UserID)).ReturnsAsync(getUserResponseData);
            mockProjectService.Setup(p => p.GetByID(updateProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);
            mockProjectUserService.Setup(p => p.CheckCombination(updateProjectUser.UserID, updateProjectUser.ProjectID)).ReturnsAsync(false);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Put(new Guid("793804B3-3A7C-40E4-DE67-08DC119A7DBE"), updateProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// UpdateProjectUserFailed
        /// </summary>

        [Fact]
        public void UpdateProjectUser_UpdateProjectUserFailed_ReturnsBadRequest()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(updateProjectUser.UserID)).ReturnsAsync(getUserResponseData);
            mockProjectService.Setup(p => p.GetByID(updateProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);
            mockProjectUserService.Setup(p => p.CheckCombination(updateProjectUser.UserID, updateProjectUser.ProjectID)).ReturnsAsync(true);
            mockProjectUserService.Setup(p => p.UpdateProjectUser(updateProjectUser)).ReturnsAsync(projectUserNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Put(new Guid("793804B3-3A7C-40E4-DE67-08DC119A7DBE"), updateProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<BadRequestObjectResult>(projectUserResult);
        }
        /// <summary>
        /// UpdateProjectUserSuccessfull
        /// </summary>
        [Fact]
        public void UpdateProjectUser_UpdateProjectUserSuccessfull_ReturnsNoContent()
        {
            //Arrange
            mockUserService.Setup(p => p.GetByID(updateProjectUser.UserID)).ReturnsAsync(getUserResponseData);
            mockProjectService.Setup(p => p.GetByID(updateProjectUser.ProjectID)).ReturnsAsync(getProjectIsNotNull);
            mockProjectUserService.Setup(p => p.CheckCombination(updateProjectUser.UserID, updateProjectUser.ProjectID)).ReturnsAsync(true);
            mockProjectUserService.Setup(p => p.UpdateProjectUser(updateProjectUser)).ReturnsAsync(projectUserNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Put(updateProjectUser.Id, updateProjectUser);

            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<NoContentResult>(projectUserResult);
        }
        #endregion

        #region Delete

        /// <summary>
        /// ProjectUser Deleted failed
        /// </summary>
        [Fact]
        public void Delete_ProjectDeletedFailed_ReturnNotFound()
        {
            //Arrange
            mockProjectUserService.Setup(p => p.RemoveProjectUser(updateProjectUser.Id)).ReturnsAsync(projectUserNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Delete(updateProjectUser.Id);


            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<NotFoundObjectResult>(projectUserResult);
        }


        /// <summary>
        /// ProjectUser Deleted Successfully
        /// </summary>

        [Fact]
        public void Delete_ProjectUserDeletedSuccessfully_ReturnOk()
        {
            //Arrange
            mockProjectUserService.Setup(p => p.RemoveProjectUser(updateProjectUser.Id)).ReturnsAsync(projectUserNotNull);

            //Act
            ProjectUserController projectUserController = new ProjectUserController(mockLogger.Object, mockProjectService.Object, mockUserService.Object, mockProjectUserService.Object);
            var projectUserList = projectUserController.Delete(updateProjectUser.Id);


            //Assert
            var projectUserResult = projectUserList.Result;
            Assert.IsType<OkObjectResult>(projectUserResult);
        }

        #endregion

    }

}
