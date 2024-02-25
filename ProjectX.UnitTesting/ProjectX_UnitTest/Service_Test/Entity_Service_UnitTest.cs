using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Application.Usecases.Package;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class Entity_Service_UnitTest
    {
        #region Property
        public Mock<IEntity> mockEntityRepository = new Mock<IEntity>();
        public Mock<IProject> mockProjectRepository = new Mock<IProject>();
        public Mock<IMapper> mockMapper = new Mock<IMapper>();
        public Mock<ICryptography> mockCryptography = new Mock<ICryptography>();
        private readonly List<GetEntityResponse> GetEntityResponseList = EntityMaster.GetEntityResponseListIsNotNull();
        private readonly List<GetEntityResponse> GetEntityResponseListNull = new List<GetEntityResponse>();
        private readonly GetEntityResponse EntityResponseNotNull = EntityMaster.EntityResponseNotNull();
        private readonly GetEntityResponse EntityResponseNull = null;
        private readonly Entity entityNotNull = EntityMaster.EntityNotNull();
        private readonly Entity EntityCountZero = new Entity();
        private readonly Entity entityNull = null;
        private readonly Project projectNotNull = PackageMaster.ProjectNotNull();
        private readonly Project? projectNull = null;
        private readonly List<Entity> entityListNotNull = EntityMaster.EntityListNotNull();
        private readonly List<Entity> entityListCountZero = new List<Entity>();
        private readonly EntityAddRequest entityAddRequestNotNull = EntityMaster.EntityAddNotNull();
        private readonly GetProjectResponse getprojectResponseNotNull = PackageMaster.GetProjectResponseNotNull();
        private readonly GetProjectResponse getprojectResponseNull = null;
        private readonly EntityUpdateRequest entityUpdateRequestNotNull = EntityMaster.EntityUpdateNotNull();
        #endregion

        #region GetAll
        /// <summary>
        /// Get EntityResponseList NotNull
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnEntityResponseList()
        {
            // Arrange
            mockEntityRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(entityListNotNull);
            mockMapper.Setup(m => m.Map<List<GetEntityResponse>>(entityListNotNull)).Returns(GetEntityResponseList);
            // Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.GetAll();
            // Assert
            Assert.Equal(GetEntityResponseList, entityResult.Result);
        }
        /// <summary>
        /// Get EntityResponseList Null
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnEntityResponseListIsNull()
        {
            // Arrange
            mockEntityRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(entityListCountZero);
            mockMapper.Setup(m => m.Map<List<GetEntityResponse>>(entityListNotNull)).Returns(GetEntityResponseListNull);
            // Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.GetAll();
            // Assert
            Assert.Equal(GetEntityResponseListNull, entityResult.Result);
        }
        #endregion

        #region GetById
        /// <summary>
        /// Get EntityData NotNull By Id
        /// </summary>
        [Fact]
        public void Get_GetEntityById_ReturnEntityData()
        {
            //Arrange
            mockEntityRepository.Setup(p => p.GetByIdAsync(entityNotNull.Id)).ReturnsAsync(entityNotNull);
            mockMapper.Setup(m => m.Map<GetEntityResponse>(entityNotNull)).Returns(EntityResponseNotNull);
            //Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.GetByID(entityNotNull.Id);
            //Assert
            Assert.Equal(EntityResponseNotNull, entityResult.Result);
        }
        /// <summary>
        /// Get EntityData Null By Id
        /// </summary>
        [Fact]
        public void Get_GetEntityById_ReturnEntityDataAsNull()
        {
            //Arrange
            mockEntityRepository.Setup(p => p.GetByIdAsync(entityNotNull.Id)).ReturnsAsync(entityNull);
            mockMapper.Setup(m => m.Map<GetEntityResponse>(entityNotNull)).Returns(EntityResponseNull);
            //Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.GetByID(entityNotNull.Id);
            //Assert
            Assert.Equal(EntityResponseNull, entityResult.Result);
        }
        #endregion

        #region Post
        /// <summary> 
        /// Entity Added Successfully
        /// </summary>
        /// 
        [Fact]
        public void Post_EntityAddedSuccessfully_ReturnEntityData()
        {
            //Arrange
            mockEntityRepository.Setup(p => p.AddAsync(entityNotNull)).ReturnsAsync(entityNotNull);
            mockMapper.Setup(m => m.Map<Entity>(entityAddRequestNotNull)).Returns(entityNotNull);
            mockCryptography.Setup(c => c.SaveFile(entityAddRequestNotNull.File, 0, entityNotNull.ThumbnailUrl)).ReturnsAsync(entityNotNull.ThumbnailUrl);
            //Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.AddEntity(entityAddRequestNotNull);
            //Assert
            Assert.Equal(entityNotNull, entityResult.Result);
        }
        /// <summary> 
        /// Entity Added Failed
        /// </summary>
        /// 
        [Fact]
        public void Post_EntityAddedFailed_ReturnNullResponse()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Entity>(entityAddRequestNotNull)).Returns(entityNotNull);
            mockCryptography.Setup(c => c.SaveFile(entityAddRequestNotNull.File, 0, entityNotNull.ThumbnailUrl)).ReturnsAsync(entityNotNull.ThumbnailUrl);
            mockEntityRepository.Setup(p => p.AddAsync(entityNotNull)).ReturnsAsync(entityNull);           
            //Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.AddEntity(entityAddRequestNotNull);
            //Assert
            Assert.Equal(entityNull, entityResult.Result);
        }

        /// <summary>
        ///  Check ProjectId Exist 
        /// </summary>
        [Fact]
        public void Post_CheckProjectIdExist_ReturnProject()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNotNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNotNull)).Returns(getprojectResponseNotNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(getprojectResponseNotNull.ID);

            //Assert
            Assert.Equal(getprojectResponseNotNull, projectResult.Result);
        }
        /// <summary>
        ///  Check ProjectId NotExist 
        /// </summary>
        [Fact]
        public void Post_CheckProjectIdExist_ReturnProjectAsNull()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNull)).Returns(getprojectResponseNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(getprojectResponseNotNull.ID);

            //Assert
            Assert.Equal(getprojectResponseNull, projectResult.Result);
        }
        #endregion

        #region Put
        /// <summary>
        ///  Check ProjectId Exist 
        /// </summary>
        [Fact]
        public void Put_CheckProjectIdExist_ReturnProject()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNotNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNotNull)).Returns(getprojectResponseNotNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(getprojectResponseNotNull.ID);

            //Assert
            Assert.Equal(getprojectResponseNotNull, projectResult.Result);
        }
        /// <summary>
        ///  Check ProjectId NotExist 
        /// </summary>
        [Fact]
        public void Put_CheckProjectIdExist_ReturnProjectAsNull()
        {
            //Arrange
            mockProjectRepository.Setup(p => p.GetByIdAsync(projectNotNull.Id)).ReturnsAsync(projectNull);
            mockMapper.Setup(m => m.Map<GetProjectResponse>(projectNull)).Returns(getprojectResponseNull);

            //Act
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object, mockCryptography.Object);
            var projectResult = projectService.GetByID(getprojectResponseNotNull.ID);

            //Assert
            Assert.Equal(getprojectResponseNull, projectResult.Result);
        }

        /// <summary>
        /// Entity Updated Successfully
        /// </summary>
        [Fact]
        public void Put_EntityUpdatedSuccessfully_ReturnEntityData()
        {
            //Arrange           
            mockMapper.Setup(m => m.Map<Entity>(entityUpdateRequestNotNull)).Returns(entityNotNull);
            mockCryptography.Setup(c => c.SaveFile(entityUpdateRequestNotNull.File, 1, entityNotNull.ThumbnailUrl)).ReturnsAsync(entityNotNull.ThumbnailUrl);
            mockEntityRepository.Setup(p => p.GetByIdAsync(entityUpdateRequestNotNull.Id)).ReturnsAsync(entityNotNull);
            mockEntityRepository.Setup(p => p.UpdateAsync(entityNotNull)).ReturnsAsync(entityNotNull);

            //Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.UpdateEntity(entityUpdateRequestNotNull);

            //Assert
            Assert.Equal(entityNotNull, entityResult.Result);
        }

        /// <summary>
        /// Entity Updated Failed
        /// </summary>
        [Fact]
        public void Put_EntityUpdatedFailed_ReturnEntityData()
        {
            //Arrange           
            mockMapper.Setup(m => m.Map<Entity>(entityUpdateRequestNotNull)).Returns(entityNotNull);
            mockCryptography.Setup(c => c.SaveFile(entityUpdateRequestNotNull.File, 1, entityNotNull.ThumbnailUrl)).ReturnsAsync(entityNotNull.ThumbnailUrl);
            mockEntityRepository.Setup(p => p.GetByIdAsync(entityUpdateRequestNotNull.Id)).ReturnsAsync(entityNull);
            mockEntityRepository.Setup(p => p.UpdateAsync(entityNotNull)).ReturnsAsync(EntityCountZero);

            //Act
            EntityService entityService = new EntityService(mockEntityRepository.Object, mockCryptography.Object, mockMapper.Object);
            var entityResult = entityService.UpdateEntity(entityUpdateRequestNotNull).Result;

            //Assert
            Assert.Equal(EntityCountZero.ToString(), entityResult.ToString());
        }
        #endregion
    }
}
