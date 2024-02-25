using ProjectX.Application.Usecases.Clients;

namespace ProjectX.WebApplication.IService
{
    public interface IClientService
    {
        Task<IEnumerable<GetClientResponse>> GetClientList(string accessKey);
        Task<int> AddClient(string accessKey, ClientAddRequest clientRequest);
        Task<int> UpdateClient(string accessKey, ClientUpdateRequest updateClientRequest);
        Task<int> DeleteClient(string accessKey, Guid clientID);
        Task<ClientUpdateRequest> GetClientById(string accessKey, Guid clientID);
    }
}
