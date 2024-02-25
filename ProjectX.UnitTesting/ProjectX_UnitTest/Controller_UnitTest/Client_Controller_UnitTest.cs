
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;
using ProjectX.WebAPI.Controllers;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Controller_UnitTest
{
    public class Client_Controller_UnitTest
    {
        #region Property
        public Mock<IClientService> mockClientService = new Mock<IClientService>();
        public Mock<ILogger<ClientController>> mockLogger = new Mock<ILogger<ClientController>>();
        private readonly List<GetClientResponse> getClientResponsesList = ClientMaster.ClientListIsNotNull();
        private readonly List<GetClientResponse> getClientResponsesListNull = null;
        private readonly Client clientNotNull = ClientMaster.ClientNotNull();
        private readonly Client clientNull = null;
        private readonly GetClientResponse getClientNotNull = ClientMaster.GetClientResponseNotNull();
        private readonly GetClientResponse getClientNull = null;
        private readonly ClientAddRequest addClientNotNull = ClientMaster.ClientAddRequestNotNull();
        private readonly ClientUpdateRequest updateClientNotNull = ClientMaster.ClientUpdateRequestNotNull();
        #endregion

        #region Get

        /// <summary>
        /// Get all the client list
        /// </summary>
        [Fact]
        public void Get_GetAllClient_ReturnsOkResult()
        {
            //Arrange
            mockClientService.Setup(p => p.GetAll()).ReturnsAsync(getClientResponsesList);

            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Get();

            //Assert
            var result = clientList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Get all the client list null
        /// </summary>
        [Fact]
        public void Get_GetAllClient_ReturnsNotFound()
        {
            //Arrange
            mockClientService.Setup(p => p.GetAll()).ReturnsAsync(getClientResponsesListNull);

            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Get();

            //Assert
            var result = clientList.Result;
            Assert.IsType<NotFoundObjectResult>(result);

        }
        #endregion

        #region Get By Id
        /// <summary>
        /// Get client by its Id 
        /// </summary>
        [Fact]
        public void Get_GetClientByID_ReturnsOkResult()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(getClientNotNull.ID)).ReturnsAsync(getClientNotNull);

            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Get(getClientNotNull.ID);

            //Assert
            var result = clientList.Result;
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Get all the client list null
        /// </summary>
        [Fact]
        public void Get_GetClientByID_ReturnsNotFound()
        {
            //Arrange
            mockClientService.Setup(p => p.GetByID(getClientNotNull.ID)).ReturnsAsync(getClientNull);

            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Get(getClientNotNull.ID);

            //Assert
            var result = clientList.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #region Post
        /// <summary>
        /// Client Added Successfully
        /// </summary>
        [Fact]
        public void Post_ClientAddedSuccessfully_ReturnsCreatedAtActionResult()
        {
            //Arrange
            mockClientService.Setup(p=>p.AddClient(addClientNotNull)).ReturnsAsync(clientNotNull);
            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Post(addClientNotNull);

            //Assert
            var result = clientList.Result;
            Assert.IsType<CreatedAtActionResult>(result);
        }
        /// <summary>
        /// Client added failed
        /// </summary>
        [Fact]
        public void Post_ClientAddedFailed_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.AddClient(addClientNotNull)).ReturnsAsync(clientNull);
            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Post(addClientNotNull);

            //Assert
            var result = clientList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Put

        /// <summary>
        /// Invalid Client ID
        /// </summary>
        [Fact]
        public void Put_InvalidClientID_ReturnsBadRequest()
        {
            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Put(new Guid("DAF5ED32-FEF2-4416-0252-08DC11BB4356"), updateClientNotNull);

            //Assert
            var result = clientList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Client Added Successfully
        /// </summary>
        [Fact]
        public void Put_ClientUpdatedSuccessfully_ReturnsNoContent()
        {
            //Arrange
            mockClientService.Setup(p => p.UpdateClient(updateClientNotNull)).ReturnsAsync(clientNotNull);
           
            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Put(updateClientNotNull.Id,updateClientNotNull);

            //Assert
            var result = clientList.Result;
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Client added failed
        /// </summary>
        [Fact]
        public void Post_ClientUpdatedFailed_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.UpdateClient(updateClientNotNull)).ReturnsAsync(clientNull);
           
            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Put(updateClientNotNull.Id, updateClientNotNull);

            //Assert
            var result = clientList.Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete

        /// <summary>
        /// Client Added Successfully
        /// </summary>
        [Fact]
        public void Delete_ClientDeletedSuccessfully_ReturnsOkresult()
        {
            //Arrange
            mockClientService.Setup(p => p.RemoveClient(updateClientNotNull.Id)).ReturnsAsync(clientNotNull);

            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Delete(updateClientNotNull.Id);

            //Assert
            var result = clientList.Result;
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Client added failed
        /// </summary>
        [Fact]
        public void Delete_ClientDeletedFailed_ReturnsBadRequest()
        {
            //Arrange
            mockClientService.Setup(p => p.RemoveClient(updateClientNotNull.Id)).ReturnsAsync(clientNull);

            //Act
            ClientController clientsController = new ClientController(mockLogger.Object, mockClientService.Object);
            var clientList = clientsController.Delete(updateClientNotNull.Id);

            //Assert
            var result = clientList.Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
