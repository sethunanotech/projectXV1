using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class DropDown_Service_UnitTest
    {
        #region Property
        public Mock<IClient> mockClientRepository = new Mock<IClient>();
        public Mock<IProject> mockProjectRepository = new Mock<IProject>();
        public Mock<IProjectUser> mockProjectUserRepository = new Mock<IProjectUser>();
        public Mock<IUser> mockUserRepository = new Mock<IUser>();
        private readonly List<DropDownModel> dropDownModelsList = DropDownMaster.dropDownModelsList();
        private readonly List<DropDownModel> dropDownModelsListNull = null;
        private readonly Project projectNotNull = ProjectMaster.ProjectNotNull();
        #endregion

        #region GetClientList
        /// <summary>
        /// Get ClientList NotNull
        /// </summary>        
        [Fact]
        public void GetClientDropDown_ClientDropdownList_ReturnsClientList()
        {
            //Arrange
            mockClientRepository.Setup(x => x.ClientList()).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var clientListResult = dropDownService.GetClientDropdownList();
            //Assert
            Assert.Equal(dropDownModelsList, clientListResult.Result);
        }

        /// <summary>
        /// Get ClientList Null
        /// </summary>         
        [Fact]
        public void GetClientDropDown_ClientDropdownList_ReturnsClientListAsNull()
        {
            //Arrange
            mockClientRepository.Setup(x => x.ClientList()).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var clientListResult = dropDownService.GetClientDropdownList();
            //Assert
            Assert.Equal(dropDownModelsListNull, clientListResult.Result);
        }
        #endregion

        #region GetProjectList
        /// <summary>
        /// Get ProjectList NotNull
        /// </summary>        
        [Fact]
        public void GetProjectDropDown_ProjectDropdownList_ReturnsProjectList()
        {
            //Arrange
            mockProjectRepository.Setup(x => x.ProjectList()).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var projectListResult = dropDownService.GetProjectDropdownList();
            //Assert
            Assert.Equal(dropDownModelsList, projectListResult.Result);
        }

        /// <summary>
        /// Get ProjectList Null
        /// </summary>         
        [Fact]
        public void GetProjectDropDown_ProjectDropdownList_ReturnsProjectListAsNull()
        {
            //Arrange
            mockProjectRepository.Setup(x => x.ProjectList()).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var projectListResult = dropDownService.GetClientDropdownList();
            //Assert
            Assert.Equal(dropDownModelsListNull, projectListResult.Result);
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// Get UserList NotNull
        /// </summary>        
        [Fact]
        public void GetUserDropDown_UserDropdownList_ReturnsUserList()
        {
            //Arrange
            mockUserRepository.Setup(x => x.UserList()).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var userListResult = dropDownService.GetUserDropdownList();
            //Assert
            Assert.Equal(dropDownModelsList, userListResult.Result);
        }

        /// <summary>
        /// Get ProjectList Null
        /// </summary>         
        [Fact]
        public void GetUserDropDown_UserDropdownList_ReturnsUserListAsNull()
        {
            //Arrange
            mockUserRepository.Setup(x => x.UserList()).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var userListResult = dropDownService.GetUserDropdownList();
            //Assert
            Assert.Equal(dropDownModelsListNull, userListResult.Result);
        }
        #endregion

        #region CheckProjectUserDropdown
        /// <summary>
        /// Get ProjectUserDropDownList NotNull
        /// </summary>      
        [Fact]
        public void GetProjectUserDropdown_CheckProjectUserDropdown_ReturnsDropdownList()
        {
            //Arrange
            mockProjectUserRepository.Setup(x => x.CheckProjectUserExist(projectNotNull.Id)).ReturnsAsync(dropDownModelsList);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var clientListResult = dropDownService.CheckProjectUserDropdown(projectNotNull.Id);
            //Assert
            Assert.Equal(dropDownModelsList, clientListResult.Result);
        }
        /// <summary>
        /// Get ProjectUserDropDownList Null
        /// </summary>      
        [Fact]
        public void GetProjectUserDropdown_CheckProjectUserDropdown_ReturnsDropdownListASNull()
        {
            //Arrange
            mockProjectUserRepository.Setup(x => x.CheckProjectUserExist(projectNotNull.Id)).ReturnsAsync(dropDownModelsListNull);
            //Act
            DropDownService dropDownService = new DropDownService(mockClientRepository.Object, mockProjectUserRepository.Object, mockProjectRepository.Object, mockUserRepository.Object);
            var clientListResult = dropDownService.CheckProjectUserDropdown(projectNotNull.Id);
            //Assert
            Assert.Equal(dropDownModelsListNull, clientListResult.Result);
        }
        #endregion
    }
}
