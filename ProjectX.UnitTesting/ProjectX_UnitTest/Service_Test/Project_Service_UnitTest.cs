using AutoMapper;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using ProjectX.UnitTesting.Model;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class Project_Service_UnitTest
    {
        public Mock<IProject> mockProjectRepository = new Mock<IProject>();
        public Mock<IClient> mockClientRepository = new Mock<IClient>();

        public Mock<IMapper> mockMapper = new Mock<IMapper>();
        public Mock<ICryptography> mockCryptography = new Mock<ICryptography>();

        private readonly List<GetProjectResponse> getProjectResponseNotNull = ProjectMaster.ProjectListIsNotNull();
        private readonly GetProjectResponse getProjectIsNotNull = ProjectMaster.ProjectIsNotNull();
        private readonly GetProjectResponse getProjectIsNull = null;
        private readonly List<GetProjectResponse> getProjectResponseListNull = null;
        private readonly ProjectUpdateRequest projectUpdateRequestNotNull = ProjectMaster.ProjectUpdateRequestNotNull();
        private readonly Client clientNotNull = ProjectMaster.ClientNotNull();
        private readonly Client clientNull = null;
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        private readonly List<Project> projectListNotNull = ProjectMaster.ListOfProjects();
        private readonly List<Project> projectListNull = null;

        private readonly Project projectNull = null;
        private readonly ProjectAddRequest projectAddRequestNotNull = ProjectMaster.AddProjectRequestNotNull();
        private readonly ProjectAddRequest projectAddRequestNull = null;




        #region GetProject
        /// <summary>
        /// ProjectListNull
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnProjectListNull()
        {
            // Arrange
            mockProjectRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(projectListNotNull);
            mockMapper.Setup(m => m.Map<List<GetProjectResponse>>(projectNotNull)).Returns(getProjectResponseNotNull);
            // Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetAll();

            //Assert
            Assert.Equal(getProjectResponseListNull, projectResult.Result);
        }
        /// <summary>
        /// ProjectListNotNul
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnProjectListNotNull()
        {
            // Arrange
            mockProjectRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(projectListNotNull);
            mockMapper.Setup(m => m.Map<List<GetProjectResponse>>(projectListNotNull)).Returns(getProjectResponseNotNull);
            // Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetAll();
            //Assert
            Assert.Equal(getProjectResponseNotNull, projectResult.Result);

        }
        #endregion


        #region GetProjectById

        /// <summary>
        /// GetByIDNull
        /// </summary>
        [Fact]
        public void Get_GetByID_ReturnNull()
        {
            //Arrange
          
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNotNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNotNull)).Returns(getProjectIsNotNull);
            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(projectNotNull.Id);

            //Assert
            Assert.Equal(getProjectIsNotNull, projectResult.Result);
        }
        /// <summary>
        /// GetByIDNotNull
        /// </summary>
        [Fact]
        public void Get_GetByID_ReturnProjectData()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNotNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNotNull)).Returns(getProjectIsNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(projectNotNull.Id);
            //Assert
            Assert.Equal(getProjectIsNull, projectResult.Result);

        }
        #endregion

        #region Post
        /// <summary>
        /// AddedFailed
        /// </summary>
        [Fact]
        public void Post_AddedFailed_ReturnNull()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Project>(projectAddRequestNotNull)).Returns(projectNotNull);
            mockProjectRepository.Setup(p => p.AddAsync(projectNull)).ReturnsAsync(projectNull);
            mockCryptography.Setup(p => p.Encrypt(projectNotNull.Title)).Returns(projectNotNull.SecretCode);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.AddProject(projectAddRequestNotNull);

            //Assert

            Assert.Equal(projectNull, projectResult.Result);

        }
        /// <summary>
        /// AddedSuccessfully
        /// </summary>
       
        [Fact]
        public void Post_AddedSuccessfully_ReturnNotNull()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Project>(projectAddRequestNotNull)).Returns(projectNotNull);
            mockProjectRepository.Setup(p => p.AddAsync(projectNotNull)).ReturnsAsync(projectNotNull);
            mockCryptography.Setup(p => p.Encrypt(projectNotNull.Title)).Returns(projectNotNull.SecretCode);
            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.AddProject(projectAddRequestNotNull);

            //Assert

            Assert.Equal(projectNotNull, projectResult.Result);

        }
        #endregion


        #region Put
        /// <summary>
        /// UpdateFailed
        /// </summary>
        [Fact]
        public void Put_UpdateFailed_ReturnNull()
        { 
            //Arrange
            mockMapper.Setup(m => m.Map<Project>(projectUpdateRequestNotNull)).Returns(projectNull);
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNull);
            mockProjectRepository.Setup(p => p.UpdateAsync(projectNotNull)).ReturnsAsync(projectNull);
            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.UpdateProject(projectUpdateRequestNotNull);

            //Assert

            Assert.Equal(projectNull, projectResult.Result);

        }
        /// <summary>
        /// UpdateSuccessfully
        /// </summary>
        [Fact]
        public void Put_UpdateSuccessfully_ReturnNotNull()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Project>(projectUpdateRequestNotNull)).Returns(projectNotNull);
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNotNull);
            mockProjectRepository.Setup(p => p.UpdateAsync(projectNotNull)).ReturnsAsync(projectNotNull);
            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.UpdateProject(projectUpdateRequestNotNull);

            //Assert

            Assert.Equal(projectNotNull, projectResult.Result);

        }
        #endregion

        #region Delete

        /// <summary>
        /// DeletedFailed
        /// </summary>
        [Fact]
        public void Delete_RemoveProject_ReturnDeletedFailed()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNull);
            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.RemoveProject(projectNotNull.Id);
            //Assert
            Assert.Equal(projectNull, projectResult.Result);

        }
        /// <summary>
        /// DeletedSuccessfully
        /// </summary>
        [Fact]
        public void Delete_RemoveProject_ReturnDeletedSuccessfully()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNotNull);
            mockProjectRepository.Setup(p => p.RemoveByIdAsync(projectNotNull.Id));
            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.RemoveProject(projectNotNull.Id);

            //Assert
            Assert.Equal(projectNotNull, projectResult.Result);

        }
        #endregion
    }



}
