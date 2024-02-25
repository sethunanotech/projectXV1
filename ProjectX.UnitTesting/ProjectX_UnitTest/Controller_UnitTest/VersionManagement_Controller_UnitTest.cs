using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.Login;
using ProjectX.Application.Usecases.User;
using ProjectX.Application.Usecases.VersionManagement;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class VersionManagement_Controller_UnitTest
    {
        #region Property
        public Mock<IVersionManagementService> mockVersionManagementService = new Mock<IVersionManagementService>();
        public Mock<ILogger<VersionManagementController>> mockLogger = new Mock<ILogger<VersionManagementController>>();
        private readonly GetUpdatesRequest getUpdatesRequestNotNull = VersionManagementMaster.GetUpdatesRequestNotNull();
        private readonly GetUpdatesRequest getUpdatesRequestProjectIdEmpty = VersionManagementMaster.GetUpdatesRequestProjectIdEmpty();
        private readonly GetUpdatesRequest getUpdatesRequestVersionZero = VersionManagementMaster.GetUpdatesRequestProjectIdVersionZero();
        private readonly GetUpdatesRequest getUpdatesRequestVersionNegative = VersionManagementMaster.GetUpdatesRequestProjectIdVersionNegative();
        private readonly List<GetUpdateResponse> getUpdateResponseList = VersionManagementMaster.GetUpdateResponseList();
        #endregion

        #region
        /// <summary>
        ///  Requested projectID is empty
        /// </summary>
        [Fact]
        public void GetUpdatest_GetUpdatesRequestIdEmpty_ReturnBadRequest()
        {

            //Act
            VersionManagementController versionManagementController = new VersionManagementController(mockLogger.Object, mockVersionManagementService.Object);
            var getUpdates = versionManagementController.GetUpdates(getUpdatesRequestProjectIdEmpty);
           
            //Assert
            var result = getUpdates.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Requested version is zero
        /// </summary>
        [Fact]
        public void GetUpdatest_GetUpdatesRequestVersionZero_ReturnBadRequest()
        {

            //Act
            VersionManagementController versionManagementController = new VersionManagementController(mockLogger.Object, mockVersionManagementService.Object);
            var getUpdates = versionManagementController.GetUpdates(getUpdatesRequestVersionZero);
           
            //Assert
            var result = getUpdates.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Requested version is Negative
        /// </summary>
        [Fact]
        public void GetUpdatest_GetUpdatesRequestVersionNegative_ReturnBadRequest()
        {

            //Act
            VersionManagementController versionManagementController = new VersionManagementController(mockLogger.Object, mockVersionManagementService.Object);
            var getUpdates = versionManagementController.GetUpdates(getUpdatesRequestVersionNegative);
          
            //Assert
            var result = getUpdates.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        ///  Get update requset is not null
        /// </summary>
        [Fact]
        public void GetUpdatest_GetUpdatesRequestNotNull_ReturnBadRequest()
        {
            
            //Act
            VersionManagementController versionManagementController = new VersionManagementController(mockLogger.Object, mockVersionManagementService.Object);
            var getUpdates = versionManagementController.GetUpdates(getUpdatesRequestNotNull);
          
            //Assert
            var result = getUpdates.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion
    }
}
