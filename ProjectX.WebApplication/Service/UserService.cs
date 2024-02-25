using ProjectX.Application.Usecases.User;
using ProjectX.WebApplication.IService;
using System.Net;
using System.Net.Http.Headers;

namespace ProjectX.WebApplication.Service
{
    public class UserService : IUserService
    {
        private readonly ILoginService _loginService;
        public UserService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<int> AddUser(string accessKey, UserAddRequest userRequest)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.PostAsJsonAsync("User/1.0", userRequest);
                var readTask = await responseTask.Content.ReadAsAsync<GetUserResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.Created)
                {
                    status = 200;
                }
                return status;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<GetUserResponse>> GetUserList(string accessKey)
        {
            try
            {
                List<GetUserResponse> clientLists = new List<GetUserResponse>();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.GetAsync("User/1.0");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<List<GetUserResponse>>();
                    clientLists = readTask;
                }
                return clientLists;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<int> UpdateUser(string accessKey, UserUpdateRequest updateUserRequest)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.PutAsJsonAsync("User/1.0/" + updateUserRequest.Id, updateUserRequest);
                var readTask = await responseTask.Content.ReadAsAsync<GetUserResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.NoContent)
                {
                    status = 200;
                }
                return status;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<GetUserResponse> DeleteById(string accessKey, Guid userId)
        {
            try
            {
                GetUserResponse projectList = new GetUserResponse();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessKey);
                var responseTask = await httpClient.GetAsync("User/1.0" + "/" + userId);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetUserResponse>();
                    projectList = readTask;
                }
                return projectList;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<int> DeleteUser(string accessKey, Guid UserId)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.DeleteAsync("User/1.0/" + UserId);
                var readTask = await responseTask.Content.ReadAsAsync<GetUserResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    status = 200;
                }

                return status;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<UserUpdateRequest> GetUserById(string accessKey, Guid userId)
        {
            try
            {
                GetUserResponse userList = new GetUserResponse();
                UserUpdateRequest updateUserRequest = new UserUpdateRequest();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.GetAsync("User/1.0" + "/" + userId);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetUserResponse>();
                    userList = readTask;
                    updateUserRequest.Id = userList.Id;
                    updateUserRequest.ClientId = userList.ClientID;
                    updateUserRequest.UserName = userList.UserName;
                    updateUserRequest.Name = userList.Name;
                    updateUserRequest.LastModifiedBy = userList.LastModifiedBy;
                    updateUserRequest.Password=userList.Password;
                }
                return updateUserRequest;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }

}
