using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class DropDown_Controller_UnitTest
    {
        #region Property
        public Mock<IDropDownService> mockDropDownService = new Mock<IDropDownService>();
        public Mock<ILogger<DropDownController>> mockLogger = new Mock<ILogger<DropDownController>>(); 
        private readonly List<DropDownModel> dropDownModelsList = DropDownMaster.dropDownModelsList();
        private readonly List<DropDownModel> dropDownModelsListNull = null;
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        #endregion

        #region GetClientDropDown
        /// <summary>
        ///  Return ClientDropDown List
        /// </summary>  
        [Fact]
        public void Get_ReturnClientDropDownList_ReturnsOK()
        {
            //Arrange
            mockDropDownService.Setup(x => x.GetClientDropdownList()).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object,mockDropDownService.Object);
            var getClientList = dropDownController.GetClientDropDown();
            //Assert
            var result = getClientList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  Return ClientDropDown List as Null
        /// </summary>  
        [Fact]
        public void Get_ReturnClientDropDownList_ReturnsBadRequest()
        {
            //Arrange
            mockDropDownService.Setup(x => x.GetClientDropdownList()).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getClientList = dropDownController.GetClientDropDown();
            //Assert
            var result = getClientList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetProjectDropDown 
        /// <summary>
        ///  Return ProjectDropDown List
        /// </summary>  
        [Fact]
        public void Get_ReturnProjectDropDownList_ReturnsOK()
        {
            //Arrange
            mockDropDownService.Setup(x => x.GetProjectDropdownList()).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getProjectList = dropDownController.GetProjectDropDown();
            //Assert
            var result = getProjectList.Result;
            Assert.IsType<OkObjectResult>(result);
        } 
        /// <summary>
        ///  Return ProjectDropDown List as Null
        /// </summary>  
        [Fact]
        public void Get_ReturnProjectDropDownList_ReturnsBadRequest()
        {
            //Arrange
            mockDropDownService.Setup(x => x.GetProjectDropdownList()).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getProjectList = dropDownController.GetProjectDropDown();
            //Assert
            var result = getProjectList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetUserDropDown 
        /// <summary>
        ///  Return UserDropDown List
        /// </summary>  
        [Fact]
        public void Get_ReturnUserDropDownList_ReturnsOK()
        {
            //Arrange
            mockDropDownService.Setup(x => x.GetUserDropdownList()).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getUserList = dropDownController.GetUserDropDown();
            //Assert
            var result = getUserList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  Return ProjectDropDown List as Null
        /// </summary>  
        [Fact]
        public void Get_ReturnUserDropDownList_ReturnsBadRequest()
        {
            //Arrange
            mockDropDownService.Setup(x => x.GetUserDropdownList()).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getClientList = dropDownController.GetUserDropDown();
            //Assert
            var result = getClientList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetBindedUserDropDown
        /// <summary>
        ///  Return BindedUserDropDown List
        /// </summary>  
        [Fact]
        public void Get_ReturnBindedUserDropDownList_ReturnsOK()
        {
            //Arrange
            mockDropDownService.Setup(x => x.CheckProjectUserDropdown(projectNotNull.Id)).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getBinedUserList = dropDownController.GetBindedUserDropDown(projectNotNull.Id);
            //Assert
            var result = getBinedUserList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  Return BindedUserDropDown List as NUll
        /// </summary>  
        [Fact]
        public void Get_ReturnBindedUserDropDownList_ReturnsBadRequest()
        {
            //Arrange
            mockDropDownService.Setup(x => x.CheckProjectUserDropdown(projectNotNull.Id)).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownController dropDownController = new DropDownController(mockLogger.Object, mockDropDownService.Object);
            var getBinedUserList = dropDownController.GetBindedUserDropDown(projectNotNull.Id);
            //Assert
            var result = getBinedUserList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion
    }
}
