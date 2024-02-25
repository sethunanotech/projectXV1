using AutoMapper;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Package;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using ProjectX.UnitTesting.Model;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class Package_Service_UnitTest
    {
        #region Property
        public Mock<IPackage> mockPackageRepository = new Mock<IPackage>();
        public Mock<IProject> mockProjectRepository = new Mock<IProject>();
        public Mock<IMapper> mockMapper = new Mock<IMapper>();
        public Mock<ICacheService> mockCacheService = new Mock<ICacheService>();
        public Mock<ICryptography> mockCryptography = new Mock<ICryptography>();
        private readonly List<GetPackageResponse> GetPackageList = PackageMaster.GetPackageResponseList();
        private readonly List<GetPackageResponse> GetPackageListNull = new List<GetPackageResponse>();
        private readonly GetPackageResponse GetPackageResponseNotNull = PackageMaster.GetPackageResponseNotNull();
        private readonly GetPackageResponse? GetPackageResponseNUll = null;
        private readonly List<Package> PackageList = PackageMaster.PackageList();
        private readonly List<Package> packagesListCountZero = new List<Package>();
        private readonly PackageAddRequest packageAddRequestNotNull = PackageMaster.PackageAddRequestNotNull();
        private readonly PackageUpdateRequest packageUpdateRequestNotNull= PackageMaster.PackageUpdateRequestNotNull(); 
       
        private readonly Package packageNotNull = PackageMaster.PackageNotNull();
        private readonly Package? packageNull = null;
        private readonly Package packageCountZero = new Package();
        private readonly Project projectNotNull = PackageMaster.ProjectNotNull();
        private readonly Project? projectNull = null;
        private readonly GetProjectResponse getprojectResponseNotNull = PackageMaster.GetProjectResponseNotNull();
        private readonly GetProjectResponse getprojectResponseNull = null;
        #endregion

        #region GetAll
          /// <summary>
          /// Get PackageList NotNull
          /// </summary>

        [Fact]
        public void Get_GetAll_ReturnPackageList()
        {
            // Arrange
            mockPackageRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(PackageList);
            mockMapper.Setup(m => m.Map<List<GetPackageResponse>>(PackageList)).Returns(GetPackageList);
            // Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.GetAll();
            // Assert
            Assert.Equal(GetPackageList, packageResult.Result);
        }
        /// <summary>
        /// Get PackageList Null
        /// </summary>

        [Fact]
        public void Get_GetAll_ReturnPackageListAsNull()
        {
            // Arrange
            mockPackageRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(packagesListCountZero);
            mockMapper.Setup(m => m.Map<List<GetPackageResponse>>(PackageList)).Returns(GetPackageListNull);
            // Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.GetAll();
            // Assert
            Assert.Equal(GetPackageListNull, packageResult.Result);
        }
        #endregion

        #region GetById
        /// <summary>
        /// Get PackageData NotNull By Id
        /// </summary>
        
        [Fact]
        public void Get_GetUserById_ReturnPackageData()
        {
            //Arrange
            mockPackageRepository.Setup(p => p.GetByIdAsync(packageNotNull.Id)).ReturnsAsync(packageNotNull);
            mockMapper.Setup(m => m.Map<GetPackageResponse>(packageNotNull)).Returns(GetPackageResponseNotNull);

            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.GetByID(packageNotNull.Id);  
            //Assert
            Assert.Equal(GetPackageResponseNotNull, packageResult.Result);
        }
        /// <summary>
        /// Get By Id No Records Found
        /// </summary>

        [Fact]
        public void Get_GetPackageById_ReturnPackageNull()
        {
            //Arrange
            mockPackageRepository.Setup(p => p.GetByIdAsync(packageNotNull.Id)).ReturnsAsync(packageNull);
            mockMapper.Setup(m => m.Map<GetPackageResponse>(packageNotNull)).Returns(GetPackageResponseNUll);

            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.GetByID(packageNotNull.Id);
            //Assert
            Assert.Equal(GetPackageResponseNUll, packageResult.Result);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Package Removed Successfully
        /// </summary>         
        [Fact]
        public void Delete_RemovePackage_DeletedSuccessfully()
        {
            //Arrange
            mockPackageRepository.Setup(p => p.GetByIdAsync(packageNotNull.Id)).ReturnsAsync(packageNotNull);
            mockPackageRepository.Setup(p => p.RemoveByIdAsync(packageNotNull.Id));
            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.RemovePackage(packageNotNull.Id);

            //Assert
            Assert.Equal(packageNotNull, packageResult.Result);
        }

        /// <summary>
        /// Package Removed Failed
        /// </summary> 
        [Fact]
        public void Delete_RemovePackage_DeletedFailed()
        {
            //Arrange
            mockPackageRepository.Setup(p => p.GetByIdAsync(packageNotNull.Id)).ReturnsAsync(packageNull);
            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.RemovePackage(packageNotNull.Id);

            //Assert
            Assert.Equal(packageNull, packageResult.Result);
        }
        #endregion

        #region Post
        /// <summary>
        /// Package Added Successfully
        /// </summary>
        [Fact]
        public void Post_PackageAddedSuccessfully_ReturnUserData()
        {
            //Arrange
            mockPackageRepository.Setup(p => p.AddAsync(packageNotNull)).ReturnsAsync(packageNotNull);
            mockMapper.Setup(m => m.Map<Package>(packageAddRequestNotNull)).Returns(packageNotNull);
            mockCryptography.Setup(c => c.SaveFile(packageAddRequestNotNull.File, 1, packageNotNull.Url)).ReturnsAsync(packageNotNull.Url);

            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.AddPackage(packageAddRequestNotNull);

            //Assert
            Assert.Equal(packageNotNull, packageResult.Result);
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
            ProjectService projectService = new ProjectService(mockProjectRepository.Object, mockMapper.Object,mockCryptography.Object);
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

        /// <summary>
        /// Package Added Failed
        /// </summary>
        [Fact]
        public void Post_PackageAddedFailed_ReturnNullResponse()
        {
            //Arrange           
            mockMapper.Setup(m => m.Map<Package>(packageAddRequestNotNull)).Returns(packageNotNull);
            mockCryptography.Setup(c => c.SaveFile(packageAddRequestNotNull.File, packageAddRequestNotNull.Version, "")).ReturnsAsync(packageNotNull.Url);
             mockPackageRepository.Setup(p => p.AddAsync(packageNotNull)).ReturnsAsync(packageNull);
            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.AddPackage(packageAddRequestNotNull);

            //Assert
            Assert.Equal(packageNull, packageResult.Result);
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
        /// Package Updated Failed
        /// </summary>
        [Fact]
        public void Put_PackageUpdatedFailed_ReturnNullResponse()
        {
            //Arrange           
            mockMapper.Setup(m => m.Map<Package>(packageUpdateRequestNotNull)).Returns(packageNotNull);
            mockCryptography.Setup(c => c.SaveFile(packageUpdateRequestNotNull.File, packageUpdateRequestNotNull.Version, "")).ReturnsAsync(packageNotNull.Url);
            mockPackageRepository.Setup(p => p.GetByIdAsync(packageUpdateRequestNotNull.Id)).ReturnsAsync(packageNull);
            mockPackageRepository.Setup(p => p.UpdateAsync(packageNotNull)).ReturnsAsync(packageCountZero);
            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.UpdatePackage(packageUpdateRequestNotNull).Result;

            //Assert
            Assert.Equal(packageCountZero.ToString(), packageResult.ToString());
        }

        /// <summary>
        /// Package Updated Successfully
        /// </summary>
        [Fact]
        public void Put_PackageUpdatedSuccessfully_ReturnUserData()
        {
            //Arrange           
            mockMapper.Setup(m => m.Map<Package>(packageUpdateRequestNotNull)).Returns(packageNotNull);
            mockCryptography.Setup(c => c.SaveFile(packageUpdateRequestNotNull.File, 1, packageNotNull.Url)).ReturnsAsync(packageNotNull.Url);
            mockPackageRepository.Setup(p => p.GetByIdAsync(packageUpdateRequestNotNull.Id)).ReturnsAsync(packageNotNull);
            mockPackageRepository.Setup(p => p.UpdateAsync(packageNotNull)).ReturnsAsync(packageNotNull);

            //Act
            PackageService packageService = new PackageService(mockPackageRepository.Object, mockMapper.Object, mockCryptography.Object, mockCacheService.Object);
            var packageResult = packageService.UpdatePackage(packageUpdateRequestNotNull);

            //Assert
            Assert.Equal(packageNotNull, packageResult.Result);
        }

        #endregion
    }
}
