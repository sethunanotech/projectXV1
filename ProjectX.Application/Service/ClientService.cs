using AutoMapper;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Service
{
    public class ClientService : IClientService
    {
        private readonly IClient _clientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClient clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        
        public async Task<Client> AddClient(ClientAddRequest clientAddRequest)
        {
           var clientData = _mapper.Map<Client>(clientAddRequest);
            var client = await _clientRepository.AddAsync(clientData);
            return client;
        }

        public async Task<Client> RemoveClient(Guid Id)
        {
            var client = await _clientRepository.GetByIdAsync(Id);
            if (client == null)
            {
                return null;
            }
            await _clientRepository.RemoveByIdAsync(client.Id);
            return client;
        }

        public async Task<IEnumerable<GetClientResponse>> GetAll()
        {
            List<GetClientResponse> getClientResponses = new List<GetClientResponse>();
            var IsClientExist = await _clientRepository.GetAllAsync();
            if (IsClientExist.Count() > 0)
                getClientResponses = _mapper.Map<List<GetClientResponse>>(IsClientExist);
            return getClientResponses;
        }

        public async Task<GetClientResponse> GetByID(Guid ID)
        {
            var clients = await _clientRepository.GetByIdAsync(ID);
            var getClients = _mapper.Map<GetClientResponse>(clients);
            return getClients;
        }

        public async Task<Client> UpdateClient(ClientUpdateRequest clientUpdateRequest)
        {
          var client = _mapper.Map<Client>(clientUpdateRequest);
            var existingClient = await _clientRepository.GetByIdAsync(clientUpdateRequest.Id); 
            if (existingClient != null)
            {
                client.CreatedOn = existingClient.CreatedOn;
                client.CreatedBy = existingClient.CreatedBy;
            }
            var updatedClient = await _clientRepository.UpdateAsync(client);
            return updatedClient;
        }
    }
}
