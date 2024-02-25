using ProjectX.Application.Usecases.Clients;
using ProjectX.WebApplication.IService;
using System.Net;

namespace ProjectX.WebApplication.Service
{
    public class ClientService : IClientService
    {
        private readonly ILoginService _loginService;
        public ClientService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<IEnumerable<GetClientResponse>> GetClientList(string accessKey)
        {
            IEnumerable<GetClientResponse> clientList = new List<GetClientResponse>();
            var httpclient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpclient.GetAsync("Client/1.0");
            if (responseTask.IsSuccessStatusCode)
            {
              var readTask = await responseTask.Content.ReadAsAsync<List<GetClientResponse>>();
               clientList= readTask;
            }
            return clientList;
        }
        public async Task<int> AddClient(string accessKey, ClientAddRequest clientRequest)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.PostAsJsonAsync("Client/1.0", clientRequest);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.Created)
            {
                status = 200;
            }
            return status;
        }
        public async Task<int> UpdateClient(string accessKey, ClientUpdateRequest updateClientRequest)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.PutAsJsonAsync("Client/1.0/" + updateClientRequest.Id, updateClientRequest);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                status = 200;
            }
            return status;
        }

        public async Task<int> DeleteClient(string accessKey, Guid clientID)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.DeleteAsync("Client/1.0/" + clientID);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.Created)
            {
                status = 200;
            }
            return status;
        }

        public async Task<ClientUpdateRequest> GetClientById(string accessKey, Guid clientID)
        {
            GetClientResponse clientList = new GetClientResponse();
            ClientUpdateRequest updateClientRequest = new ClientUpdateRequest();
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.GetAsync("Client/1.0" + "/" + clientID);
            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsAsync<GetClientResponse>();
                clientList = readTask;
                updateClientRequest.Id = clientID;
                updateClientRequest.Name = clientList.Name;
                updateClientRequest.Address = clientList.Address;
                updateClientRequest.LastModifiedBy = clientList.LastModifiedBy;
               
            }
            return updateClientRequest;
        }
    }
}
