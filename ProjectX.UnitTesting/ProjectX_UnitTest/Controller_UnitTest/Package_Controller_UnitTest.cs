using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Package;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class Package_Controller_UnitTest
    {

        #region Property
        public Mock<IPackageService> mockPackageService = new Mock<IPackageService>();
        public Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
        public Mock<IEntityService> mockEntityService = new Mock<IEntityService>();
        public Mock<ILogger<PackageController>> mockLogger = new Mock<ILogger<PackageController>>();
        private readonly List<GetPackageResponse> GetPackageList = PackageMaster.GetPackageResponseList();
        private readonly List<GetPackageResponse> GetPackageListNull = new List<GetPackageResponse>();
        private readonly Package packageNotNull = PackageMaster.PackageNotNull();
        private readonly Package? packageNull = null;
        private readonly GetPackageResponse getPackageResponseNotNull = PackageMaster.GetPackageResponseNotNull();
        private readonly GetPackageResponse? getPackageResponseNull = null;
        private readonly PackageAddRequest? packageAddRequestNull = null;
        private readonly PackageAddRequest packageAddRequestNotNull = PackageMaster.PackageAddRequestNotNull();
        private readonly PackageUpdateRequest packageUpdateRequestNotNull = PackageMaster.PackageUpdateRequestNotNull();
        private readonly GetProjectResponse? GetProjectResponseNull = null;
        private readonly GetProjectResponse getProjectResponseNotNull = PackageMaster.GetProjectResponseNotNull();
        #endregion

        #region Get
        /// <summary>
        ///  Return Package List
        /// </summary>         
        [Fact]
        public void Get_ReturnPackageResposne_ReturnsOK()
        {
            //Arrange
            mockPackageService.Setup(p => p.GetAll()).ReturnsAsync(GetPackageList);

            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageList = packageController.Get();
            //Assert 
            var result = packageList.Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        ///  Return Package Response as NotFound
        /// </summary>         
        [Fact]
        public void Get_ReturnPackageResposne_ReturnsNotFound()
        {
            //Arrange
            mockPackageService.Setup(p => p.GetAll()).ReturnsAsync(GetPackageListNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageList = packageController.Get();
            //Assert 
            var result = packageList.Result;
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region GetById
        /// <summary>
        ///  Return Package DataUsing PackageId
        /// </summary>  
        [Fact]
        public void Get_ReturnPackageResposneById_ReturnsOK()
        {
            //Arrange
            mockPackageService.Setup(p => p.GetByID(packageNotNull.Id)).ReturnsAsync(getPackageResponseNotNull);

            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageList = packageController.Get(packageNotNull.Id);
            //Assert 
            var result = packageList.Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  Invalid PackageId
        /// </summary>  
        [Fact]
        public void Get_InvalidPackageId_ReturnsNotFoundObjectResult()
        {
            //Arrange
            mockPackageService.Setup(p => p.GetByID(packageNotNull.Id)).ReturnsAsync(getPackageResponseNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Get(packageNotNull.Id);
            //Assert 
            var result = packageData.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #region Post
        /// <summary> 
        /// Package Added Successfully
        /// </summary>
        [Fact]
        public void Post_CheckIfPackageAdded_ReturnCreatedAtAction()
        {
            //Arrange
            mockProjectService.Setup(p=>p.GetByID(packageAddRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockPackageService.Setup(p => p.AddPackage(packageAddRequestNotNull)).ReturnsAsync(packageNotNull);
            //Act 
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Post(packageAddRequestNotNull);
            //Assert
            var result = packageData.Result;
            Assert.IsType<CreatedAtActionResult>(result);
        }
        /// <summary>
        /// Invalid ProjectId
        /// </summary>
        [Fact]
        public void Post_CheckProjectIdExist_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(packageAddRequestNotNull.ProjectID)).ReturnsAsync(GetProjectResponseNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Post(packageAddRequestNotNull);
            //Assert
            var result = packageData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        /// <summary>
        /// Package Added Failed
        /// </summary>
        [Fact]
        public void Post_CheckIfProjectAdded_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(packageAddRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockPackageService.Setup(p => p.AddPackage(packageAddRequestNull)).ReturnsAsync(packageNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Post(packageAddRequestNull);
            //Assert
            var result = packageData.Result;
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
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            //Act
            var packageData = packageController.Put(default, packageUpdateRequestNotNull);
            //Assert
            var updateUser = packageData.Result;
            Assert.IsType<BadRequestResult>(updateUser);
        }

        /// <summary>
        /// Package Updated Successfully
        /// 
        [Fact]
        public void Put_CheckIfPackageUpdated_ReturnNoContent()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetByID(packageUpdateRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockPackageService.Setup(p => p.UpdatePackage(packageUpdateRequestNotNull)).ReturnsAsync(packageNotNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Put(packageUpdateRequestNotNull.Id, packageUpdateRequestNotNull);
            //Assert
            var result = packageData.Result;
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Invalid ProjectId
        /// </summary>
        [Fact]
        public void Put_CheckProjectIdExist_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(u => u.GetByID(packageUpdateRequestNotNull.ProjectID)).ReturnsAsync(GetProjectResponseNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Put(packageUpdateRequestNotNull.Id,packageUpdateRequestNotNull);
            //Assert
            var result = packageData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Package Updated Failed
        /// 
        [Fact]
        public void Put_CheckIfPackageUpdated_ReturnBadRequest()
        {
            //Arrange
            mockProjectService.Setup(p => p.GetByID(packageUpdateRequestNotNull.ProjectID)).ReturnsAsync(getProjectResponseNotNull);
            mockPackageService.Setup(p => p.UpdatePackage(packageUpdateRequestNotNull)).ReturnsAsync(packageNull);
            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Put(packageUpdateRequestNotNull.Id, packageUpdateRequestNotNull);
            //Assert
            var result = packageData.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Package Deleted Successfully
        /// </summary>
        [Fact]
        public void Delete_RemovePackageById_ReturnsOk()
        {
            //Arrange
            mockPackageService.Setup(p => p.RemovePackage(packageUpdateRequestNotNull.Id)).ReturnsAsync(packageNotNull);

            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Delete(packageUpdateRequestNotNull.Id);

            //Assert
            var result = packageData.Result;
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Package Deleted failed
        /// </summary>
        [Fact]
        public void Delete_InvalidPackageId_ReturnNotFoundObjectResult()
        {
            //Arrange
            mockPackageService.Setup(p => p.RemovePackage(packageNotNull.Id)).ReturnsAsync(packageNull);

            //Act
            PackageController packageController = new PackageController(mockLogger.Object, mockPackageService.Object, mockProjectService.Object, mockEntityService.Object);
            var packageData = packageController.Delete(packageNotNull.Id);

            //Assert
            var result = packageData.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
