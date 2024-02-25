using AutoMapper;
using Moq;
using ProjectX.Application.Contracts;
using ProjectX.Application.Service;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Domain.Entities;
using ProjectX.UnitTesting.Model;

namespace ProjectX.UnitTesting.ProjectX_UnitTest.Service_Test
{
    public class Client_Service_UnitTest
    {
        #region Property
        public Mock<IClient> mockClientRepository = new Mock<IClient>();
        public Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly List<GetClientResponse> getClientResponse = ClientMaster.ClientListIsNotNull();
        private readonly List<GetClientResponse> getClientResponseNull = new List<GetClientResponse>();
        private readonly List<Client> getAllClients = ClientMaster.GetAllClient();
        private readonly List<Client> getAllClientsCountZero = new List<Client>();
        private readonly Client clientNotNull = ClientMaster.ClientNotNull();
        private readonly Client clientNull = null;
        private readonly GetClientResponse getClientNotNull = ClientMaster.GetClientResponseNotNull();
        private readonly GetClientResponse getClientNull = null;
        private readonly ClientAddRequest addClientNotNull = ClientMaster.ClientAddRequestNotNull();
        private readonly ClientUpdateRequest updateClientNotNull = ClientMaster.ClientUpdateRequestNotNull();
        #endregion

        #region Get Client
        /// <summary>
        ///  Get Client List
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnClientList()
        {
            // Arrange
            mockClientRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(getAllClients);
            mockMapper.Setup(m => m.Map<List<GetClientResponse>>(getAllClients)).Returns(getClientResponse);
           
            // Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.GetAll();

            //Assert
            Assert.Equal(getClientResponse, clientResult.Result);
        }
        /// <summary>
        ///  Get client list null
        /// </summary>
        [Fact]
        public void Get_GetAll_ReturnClientListNull()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(getAllClientsCountZero);
            mockMapper.Setup(m => m.Map<List<GetClientResponse>>(getAllClients)).Returns(getClientResponseNull);

            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.GetAll();
            //Assert
            Assert.Equal(getClientResponseNull, clientResult.Result);

        }
        #endregion

        #region Get Client by Id
        /// <summary>
        ///  Get Client List
        /// </summary>
        [Fact]
        public void Get_GetByID_ReturnClientList()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(clientNotNull.Id)).ReturnsAsync(clientNotNull);
            mockMapper.Setup(m => m.Map<GetClientResponse>(clientNotNull)).Returns(getClientNotNull);

            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.GetByID(clientNotNull.Id);

            //Assert
            Assert.Equal(getClientNotNull, clientResult.Result);

        }
        /// <summary>
        ///  Get client list null
        /// </summary>
        [Fact]
        public void Get_GetByID_ReturnClientListNull()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(clientNotNull.Id)).ReturnsAsync(clientNull);
            mockMapper.Setup(m => m.Map<GetClientResponse>(clientNotNull)).Returns(getClientNull);

            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.GetByID(clientNotNull.Id);
            //Assert
            Assert.Equal(getClientNull, clientResult.Result);

        }
        #endregion

        #region Put
        [Fact]
        public void Put_UpdateSuccessfully_ReturnClientList()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Client>(updateClientNotNull)).Returns(clientNotNull);
            mockClientRepository.Setup(p => p.GetByIdAsync(clientNotNull.Id)).ReturnsAsync(clientNotNull);
            mockClientRepository.Setup(p=>p.UpdateAsync(clientNotNull)).ReturnsAsync(clientNotNull);
            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.UpdateClient(updateClientNotNull);
            //Assert
            Assert.Equal(clientNotNull, clientResult.Result);

        }
        [Fact]
        public void Put_UpdateFailed_ReturnClientListNull()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Client>(updateClientNotNull)).Returns(clientNull);
            mockClientRepository.Setup(p => p.GetByIdAsync(clientNotNull.Id)).ReturnsAsync(clientNull);
            mockClientRepository.Setup(p => p.UpdateAsync(clientNotNull)).ReturnsAsync(clientNull);
            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.UpdateClient(updateClientNotNull);
            //Assert
            Assert.Equal(clientNull, clientResult.Result);

        }
        #endregion

        #region Post
        [Fact]
        public void Post_AddedSuccessfully_ReturnClientList()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Client>(addClientNotNull)).Returns(clientNotNull);
            mockClientRepository.Setup(p => p.AddAsync(clientNotNull)).ReturnsAsync(clientNotNull);

            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.AddClient(addClientNotNull);

            //Assert

            Assert.Equal(clientNotNull, clientResult.Result);

        }
        [Fact]
        public void Post_AddedFailed_ReturnClientListNull()
        {
            //Arrange
            mockMapper.Setup(m => m.Map<Client>(addClientNotNull)).Returns(clientNull);
            mockClientRepository.Setup(p => p.AddAsync(clientNotNull)).ReturnsAsync(clientNull);

            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.AddClient(addClientNotNull);
            //Assert
            Assert.Equal(clientNull, clientResult.Result);

        }
        #endregion


        #region Delete

        [Fact]
        public void Delete_RemoveClient_ReturnDeletedSuccessfully()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(clientNotNull.Id)).ReturnsAsync(clientNotNull);
            mockClientRepository.Setup(p => p.RemoveByIdAsync(clientNotNull.Id));
            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.RemoveClient(clientNotNull.Id);

            //Assert
            Assert.Equal(clientNotNull, clientResult.Result);

        }
        /// <summary>
        /// DeletedFailed
        /// </summary>
        [Fact]
        public void Delete_RemoveClient_ReturnDeletedFailed()
        {
            //Arrange
            mockClientRepository.Setup(p => p.GetByIdAsync(clientNotNull.Id)).ReturnsAsync(clientNull);
            //Act
            ClientService clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);
            var clientResult = clientService.RemoveClient(clientNotNull.Id);
            //Assert
            Assert.Equal(clientNull, clientResult.Result);

        }

        #endregion
    }

}
