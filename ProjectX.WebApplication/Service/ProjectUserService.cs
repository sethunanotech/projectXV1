using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Domain.Entities;
using ProjectX.WebApplication.IService;
using System.Net;

namespace ProjectX.WebApplication.Service
{
    public class ProjectUserService : IProjectUserService
    {
        private readonly ILoginService _loginService;
        public ProjectUserService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<int> AddProjectUser(string accessKey, ProjectUserAddRequest addProjectUserRequest)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.PostAsJsonAsync("ProjectUser/1.0", addProjectUserRequest);
            var statusCode = responseTask.StatusCode;            
            if (statusCode == HttpStatusCode.Created)
            {
                status = 200;
            
            }
            return status;
        }

        public async Task<int> DeleteProjectUser(string accessKey, Guid projectUserID)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.DeleteAsync("ProjectUser/1.0/" + projectUserID);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.Created)
            {
                status = 200;
            }
            return status;
        }

        public async Task<ProjectUserUpdateRequest> GetProjectUserById(string accessKey, Guid projectUserID)
        {
            GetProjectUserResponse projectUserModel = new GetProjectUserResponse();
            ProjectUserUpdateRequest updateProjectUserRequest = new ProjectUserUpdateRequest();
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.GetAsync("ProjectUser/1.0" + "/" + projectUserID);
            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsAsync<GetProjectUserResponse>();
                projectUserModel = readTask;
                updateProjectUserRequest.Id = projectUserID;
                updateProjectUserRequest.ProjectID = projectUserModel.ProjectID;
                updateProjectUserRequest.UserID = projectUserModel.UserID;
                updateProjectUserRequest.LastModifiedBy=projectUserModel.LastModifiedBy;
            }
            return updateProjectUserRequest;
        }

       

        public async Task<IEnumerable<GetProjectUserResponse>> GetProjectUserList(string accessKey)
        {
            IEnumerable<GetProjectUserResponse> projectUserList = new List<GetProjectUserResponse>();
            var httpclient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpclient.GetAsync("ProjectUser/1.0");
            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsAsync<List<GetProjectUserResponse>>();
                projectUserList = readTask;
            }
            return projectUserList;
        }

        public async Task<int> UpdateProjectUser(string accessKey, ProjectUserUpdateRequest updateProjectUserRequest)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.PutAsJsonAsync("ProjectUser/1.0/" + updateProjectUserRequest.Id, updateProjectUserRequest);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                status = 200;
            }
            return status;
        }
    }
}
