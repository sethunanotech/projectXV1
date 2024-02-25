using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class Entity_Controller_UnitTest
    {
        #region Property
        public Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
        public Mock<IEntityService> mockEntityService = new Mock<IEntityService>();
        public Mock<ILogger<EntityController>> mockLogger = new Mock<ILogger<EntityController>>();
        private readonly List<GetEntityResponse> GetEntityResponseList = EntityMaster.GetEntityResponseListIsNotNull();
        private readonly List<GetEntityResponse> GetEntityResponseListNull = new List<GetEntityResponse>();
        private readonly GetEntityResponse EntityResponseNotNull = EntityMaster.EntityResponseNotNull();
        private readonly GetEntityResponse EntityResponseNull = null; 
        private readonly GetProjectResponse? GetProjectResponseNull = null;
        private readonly GetProjectResponse getProjectResponseNotNull = PackageMaster.GetProjectResponseNotNull();
        private readonly Entity entityNotNull = EntityMaster.EntityNotNull();
        private readonly Entity entityNull = null;
        private readonly EntityAddRequest entityAddRequestNotNull = EntityMaster.EntityAddNotNull();
        private readonly EntityAddRequest entityAddRequestNull = null;
        private readonly EntityUpdateRequest entityUpdateRequestNotNull = EntityMaster.EntityUpdateNotNull();
        #endregion

        #region Get
        /// <summary>
        ///  Return Entity Response List
        /// </summary>  

        [Fact]
        public void Get_ReturnGetEntityResponseList_ReturnsOK()
        {
            //Arrange
            mockEntityService.Setup(p => p.GetAll()).ReturnsAsync(GetEntityResponseList);
            //Act
            EntityController entityController = new EntityController(mockLogger.Object,mockProjectService.Object,mockEntityService.Object);
            var entityList = entityController.Get();
            //Assert
            var result = entityList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  Return Entity Response as NotFound
        /// </summary>  
         
        [Fact]
        public void Get_ReturnGetEntityResponseList_ReturnsNotFound()
        {
            //Arrange
            mockEntityService.Setup(p => p.GetAll()).ReturnsAsync(GetEntityResponseListNull);
            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityList = entityController.Get();
            //Assert
            var result = entityList.Result;
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region GetById
        /// <summary>
        ///  Return Entity Data Using EntityID
        /// </summary>  
        [Fact]
        public void Get_ReturnGetEntityResponseById_ReturnsOK()
        {
            //Arrange
            mockEntityService.Setup(p => p.GetByID(EntityResponseNotNull.Id)).ReturnsAsync(EntityResponseNotNull);
            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityList = entityController.Get(EntityResponseNotNull.Id);
            //Assert
            var result = entityList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  Invalid EntityId
        /// </summary>  
        /// 
        [Fact]
        public void Get_InvalidEntityId_ReturnsNotFoundObjectResult()
        {
            //Arrange
            mockEntityService.Setup(p => p.GetByID(EntityResponseNotNull.Id)).ReturnsAsync(EntityResponseNull);
            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityList = entityController.Get(EntityResponseNotNull.Id);
            //Assert
            var result = entityList.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #region Post
        /// <summary>
        /// Invalid ProjectId
        /// </summary>
        [Fact]
        public void Post_CheckProjectIdExist_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(EntityResponseNotNull.ProjectID)).ReturnsAsync(GetProjectResponseNull);
            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Get(EntityResponseNotNull.Id);
            //Assert
            var result = entityData.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        /// <summary> 
        /// Entity Added Successfully
        /// </summary>
        [Fact]
        public void Post_CheckIfEntityAdded_ReturnCreatedAtAction()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(entityAddRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockEntityService.Setup(p => p.AddEntity(entityAddRequestNotNull)).ReturnsAsync(entityNotNull);
            //Act 
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Post(entityAddRequestNotNull);
            //Assert
            var result = entityData.Result;
            Assert.IsType<CreatedAtActionResult>(result);
        }

        /// <summary>
        /// Entity Added Failed
        /// </summary>
        [Fact]
        public void Post_CheckIfEntityAdded_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(entityAddRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockEntityService.Setup(p => p.AddEntity(entityAddRequestNotNull)).ReturnsAsync(entityNull);
            //Act 
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Post(entityAddRequestNull);
            //Assert
            var result = entityData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Put
        /// <summary>
        /// Requested Id Is NotEqual to Params Id
        /// </summary>
        [Fact]
        public void Put_IdIsEqual_ReturnBadRequest()
        {
            //Arrange 
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            //Act
            var entityData = entityController.Put(default, entityUpdateRequestNotNull);
            //Assert
            var updateEntity = entityData.Result;
            Assert.IsType<BadRequestResult>(updateEntity);
        }

        /// <summary> 
        /// Entity Updated  Successfully
        /// </summary>
        [Fact]
        public void Put_CheckIfEntityAdded_ReturnNoContent()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(entityAddRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockEntityService.Setup(p => p.UpdateEntity(entityUpdateRequestNotNull)).ReturnsAsync(entityNotNull);
            //Act 
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Put(entityUpdateRequestNotNull.Id, entityUpdateRequestNotNull);
            //Assert
            var result = entityData.Result;
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Invalid ProjectId
        /// </summary>
        [Fact]
        public void Put_CheckProjectIdExist_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(EntityResponseNotNull.ProjectID)).ReturnsAsync(GetProjectResponseNull);
            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Get(EntityResponseNotNull.Id);
            //Assert
            var result = entityData.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary> 
        /// Entity Updated Failed
        /// </summary>
        [Fact]
        public void Put_CheckIfEntityUpdated_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(entityAddRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockEntityService.Setup(p => p.UpdateEntity(entityUpdateRequestNotNull)).ReturnsAsync(entityNull);
            //Act 
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Put(entityUpdateRequestNotNull.Id, entityUpdateRequestNotNull);
            //Assert
            var result = entityData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Entity Deleted Successfully
        /// </summary>
        [Fact]
        public void Delete_RemoveEntityById_ReturnsOk()
        {
            //Arrange
            mockEntityService.Setup(p => p.RemoveEntity(entityNotNull.Id)).ReturnsAsync(entityNotNull);

            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Delete(entityNotNull.Id);

            //Assert
            var result = entityData.Result;
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Entity Deleted Failed
        /// </summary>
        [Fact]
        public void Delete_RemoveEntityById_ReturnsBadRequest()
        {
            //Arrange
            mockEntityService.Setup(p => p.RemoveEntity(entityNotNull.Id)).ReturnsAsync(entityNull);

            //Act
            EntityController entityController = new EntityController(mockLogger.Object, mockProjectService.Object, mockEntityService.Object);
            var entityData = entityController.Delete(entityNotNull.Id);

            //Assert
            var result = entityData.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
