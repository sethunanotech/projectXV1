using ProjectX.Application.Usecases.Projects;
using ProjectX.WebApplication.IService;
using System.Net;
using System.Net.Http.Headers;

namespace ProjectX.WebApplication.Service
{
    public class ProjectService : IProjectService
    {
        private readonly ILoginService _loginService;
        public ProjectService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<int> AddProject(string accessKey, ProjectAddRequest addProjectRequest)
        {

            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.PostAsJsonAsync("Project/1.0", addProjectRequest);
                var readTask = await responseTask.Content.ReadAsAsync<GetProjectResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.Created)
                {
                    status = 200;
                }
                return status;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public async Task<GetProjectResponse> DeleteById(string accessKey, Guid projectId)
        {
            GetProjectResponse projectModel = new GetProjectResponse();
            var httpClient = _loginService.ToaccesAPI(accessKey);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessKey);
            var responseTask = await httpClient.GetAsync("Project/1.0" + "/" + projectId);
            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsAsync<GetProjectResponse>();
                projectModel = readTask;
            }
            return projectModel;
        }

        public async Task<int> DeleteProject(string accessKey, Guid ProjectId)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.DeleteAsync("Project/1.0/" + ProjectId);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                status = 200;
            }
            return status;
        }

        public async Task<ProjectUpdateRequest> GetProjectById(string accessKey, Guid projectId)
        {            
            ProjectUpdateRequest updateProjectRequest = new ProjectUpdateRequest();
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.GetAsync("Project/1.0" + "/" + projectId);
            if (responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsAsync<GetProjectResponse>();
                GetProjectResponse projectModel = readTask;
                updateProjectRequest.Id = projectModel.ID;
                updateProjectRequest.ClientID = projectModel.ClientID;
                updateProjectRequest.Title = projectModel.Title;
                updateProjectRequest.Active = projectModel.Active;
                updateProjectRequest.LastModifiedBy = projectModel.LastModifiedBy;
                updateProjectRequest.SecretCode= projectModel.SecretCode;
            }
            return updateProjectRequest;
        }

        public async Task<IEnumerable<GetProjectResponse>> GetProjectList(string accessKey)
        {
            List<GetProjectResponse> projectList = new List<GetProjectResponse>();
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.GetAsync("Project/1.0");
            if(responseTask.IsSuccessStatusCode)
            {
                var readTask = await responseTask.Content.ReadAsAsync<List<GetProjectResponse>>();
                projectList = readTask;
            }
            return projectList;
        }

        public async Task<int> UpdateProject(string accessKey, ProjectUpdateRequest updateProjectRequest)
        {
            int status = 400;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var responseTask = await httpClient.PutAsJsonAsync("Project/1.0/" + updateProjectRequest.Id, updateProjectRequest);
            var statusCode = responseTask.StatusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                status = 200;
            }
            return status;
        }
    }
}
