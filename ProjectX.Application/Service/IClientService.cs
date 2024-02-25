using ProjectX.Application.Usecases.Clients;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Service
{
    public interface IClientService
    {
        Task<Client> AddClient(ClientAddRequest clientAddRequest);
        Task<Client> UpdateClient(ClientUpdateRequest clientUpdateRequest);
        Task<Client> RemoveClient(Guid Id);
        Task<IEnumerable<GetClientResponse>> GetAll();
        Task<GetClientResponse> GetByID(Guid ID);
    }
}
