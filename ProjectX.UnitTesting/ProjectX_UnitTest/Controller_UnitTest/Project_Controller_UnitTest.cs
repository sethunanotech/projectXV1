using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class Project_Controller_UnitTest
    {
        #region Properties
        public Mock<IClientService> mockClientService = new Mock<IClientService>();
        public Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
        public Mock<ILogger<ProjectController>> mockLogger = new Mock<ILogger<ProjectController>>();
        private readonly List<GetProjectResponse> projectListIsNotNull = ProjectMaster.ProjectListIsNotNull();
        private readonly GetProjectResponse projectIsNotNull = ProjectMaster.ProjectIsNotNull();
        private readonly GetProjectResponse projectNull = null;
        private readonly List<GetProjectResponse> projectListIsNull = null;
        private readonly ProjectUpdateRequest projectUpdateRequestNotNull = ProjectMaster.ProjectUpdateRequestNotNull();
        private readonly GetClientResponse getClientResponse = ClientMaster.GetClientResponseNotNull();
        private readonly GetClientResponse getClientResponseNull = null;
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        private readonly Project projectEntityNull = null;
        private readonly ProjectAddRequest projectAddRequestNotNull = ProjectMaster.AddProjectRequestNotNull();

        #endregion

        #region GetProjectList
        /// <summary>
        /// GetProjectListIsNull
        /// </summary>
        [Fact]
        public void GetProject_GetProjectListIsNull_ReturnNotFound()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetAll()).ReturnsAsync(projectListIsNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Get();

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<NotFoundObjectResult>(projectResult);
        }
        /// <summary>
        /// GetProjectListIsNotNull
        /// </summary>
        [Fact]
        public void GetProject_GetProjectListIsNotNull_ReturnOk()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetAll()).ReturnsAsync(projectListIsNotNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Get();

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<OkObjectResult>(projectResult);
        }
        #endregion

        #region GetProjectById
        /// <summary>
        /// GetProjectByIdIsNull
        /// </summary>
        [Fact]
        public void GetProject_GetProjectByIdIsNull_ReturnNotFound()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetByID(projectIsNotNull.ID)).ReturnsAsync(projectNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Get(projectIsNotNull.ID);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<NotFoundObjectResult>(projectResult);
        }
        /// <summary>
        /// GetProjectByIdIsNotNull
        /// </summary>
        [Fact]
        public void GetProject_GetProjectByIdIsNotNull_ReturnOk()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetByID(projectIsNotNull.ID)).ReturnsAsync(projectIsNotNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Get(projectIsNotNull.ID);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<OkObjectResult>(projectResult);
        }
        #endregion


        #region AddProject
        /// <summary>
        /// AddProjectFailed
        /// </summary>
        [Fact]
        public void Post_AddProjectFailed_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(projectAddRequestNotNull.ClientID)).ReturnsAsync(getClientResponseNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Post(projectAddRequestNotNull);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<BadRequestObjectResult>(projectResult);
        }
        /// <summary>
        /// AddProjectSuccess
        /// </summary>
        [Fact]
        public void Post_AddProjectSuccess_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(projectAddRequestNotNull.ClientID)).ReturnsAsync(getClientResponse);
            mockProjectService.Setup(p => p.AddProject(projectAddRequestNotNull)).ReturnsAsync(projectNotNull);
            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Post(projectAddRequestNotNull);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<CreatedAtActionResult>(projectResult);
        }

        #endregion

        #region UpdateProject
        /// <summary>
        /// InvalidProjectID
        /// </summary>
        [Fact]
        public void Put_InvalidProjectID_ReturnsBadRequest()
        {
            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Put(new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),projectUpdateRequestNotNull);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<BadRequestObjectResult>(projectResult);
        }
        /// <summary>
        /// InvalidClientID
        /// </summary>
        [Fact]
        public void Put_InvalidClientID_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(projectUpdateRequestNotNull.Id)).ReturnsAsync(getClientResponseNull);
            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Put(projectUpdateRequestNotNull.Id, projectUpdateRequestNotNull);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<BadRequestObjectResult>(projectResult);
        }
        /// <summary>
        /// UpdateFailed
        /// </summary>
        [Fact]
        public void Put_UpdateFailed_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(projectUpdateRequestNotNull.ClientID)).ReturnsAsync(getClientResponse);
            mockProjectService.Setup(p => p.UpdateProject(projectUpdateRequestNotNull)).ReturnsAsync(projectEntityNull);
            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Put(projectUpdateRequestNotNull.Id, projectUpdateRequestNotNull);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<BadRequestObjectResult>(projectResult);
        }
        /// <summary>
        /// Updatedsuccessfully
        /// </summary>
        [Fact]
        public void Put_Updatedsuccessfully_ReturnsNoContent()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(projectUpdateRequestNotNull.ClientID)).ReturnsAsync(getClientResponse);
            mockProjectService.Setup(p => p.UpdateProject(projectUpdateRequestNotNull)).ReturnsAsync(projectNotNull);
            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Put(projectUpdateRequestNotNull.Id, projectUpdateRequestNotNull);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<NoContentResult>(projectResult);
        }

        #endregion


        #region Delete


        /// <summary>
        /// Project Deleted failed
        /// </summary>
        [Fact]
        public void Delete_ProjectDeletedFailed_BadRequest()
        {
            //Arrange
            mockProjectService.Setup(p => p.RemoveProject(projectUpdateRequestNotNull.Id)).ReturnsAsync(projectEntityNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Delete(projectUpdateRequestNotNull.Id);

            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<NotFoundObjectResult>(projectResult);
        }

        /// <summary>
        /// Project Deleted Successfully
        /// </summary>
        [Fact]
        public void Delete_ProjectDeletedSuccessfully_NoContent()
        {
            //Arrange
            mockProjectService.Setup(p => p.RemoveProject(projectUpdateRequestNotNull.Id)).ReturnsAsync(projectNotNull);

            //Act
            ProjectController projectController = new ProjectController(mockLogger.Object, mockClientService.Object, mockProjectService.Object);
            var projectList = projectController.Delete(projectUpdateRequestNotNull.Id);


            //Assert
            var projectResult = projectList.Result;
            Assert.IsType<OkObjectResult>(projectResult);
        }

        #endregion
    }
}
