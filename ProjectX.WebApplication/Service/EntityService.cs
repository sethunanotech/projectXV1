using Azure;
using ProjectX.Application.Usecases.Entity;
using ProjectX.WebApplication.IService;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace ProjectX.WebApplication.Service
{
    public class EntityService : IEntityService
    {
        private readonly ILoginService _loginService;
        private readonly IHttpClientFactory _clientFactory;

        public EntityService(ILoginService loginService, IHttpClientFactory clientFactory)
        {
            _loginService = loginService;
            _clientFactory = clientFactory;
        }
        public async Task<int> AddEntity(string accessKey, EntityAddRequest entityAddRequest, IFormFile imageFile)
        {
            try
            {
                using var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessKey);
                int status = 400;
                //var httpClient = _loginService.ToaccesAPI(accessKey);

                var form = new MultipartFormDataContent();
                form.Add(new StreamContent(imageFile.OpenReadStream()), "file", imageFile.FileName);
                form.Add(new StringContent("ProjectID"), entityAddRequest.ProjectID.ToString());
                form.Add(new StringContent("Title"), entityAddRequest.Title); 
                form.Add(new StringContent("Active"), entityAddRequest.Active.ToString());
                form.Add(new StringContent("DisplayName"), entityAddRequest.DisplayName);
                form.Add(new StringContent("Argument1"), entityAddRequest.Argument1);
                form.Add(new StringContent("Argument2"), entityAddRequest.Argument2);
                form.Add(new StringContent("Argument3"), entityAddRequest.Argument3);
                form.Add(new StringContent("CreatedBy"), entityAddRequest.CreatedBy);

                //entityAddRequest.File = imageFile;
                var responseTask = await client.PostAsync("https://localhost:7021/api/Entity/1.0", form);
                responseTask.EnsureSuccessStatusCode();

                var readTask = await responseTask.Content.ReadAsStringAsync();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.Created)
                {
                    status = 200;
                }
                return status;
            }
            catch (Exception )
            {

                throw ;
            }
        }
       
        public  async Task<int> DeleteEntity(string accessKey, Guid entityID)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.DeleteAsync("Entity/1.0/" + entityID);
                var readTask = await responseTask.Content.ReadAsAsync<GetEntityResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.Created)
                {
                    status = 200;
                }

                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EntityUpdateRequest> GetEntityById(string accessKey, Guid entityID)
        {
            try
            {
                GetEntityResponse entityList = new GetEntityResponse();
                EntityUpdateRequest entityUpdateRequest = new EntityUpdateRequest();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.GetAsync("Entity/1.0/" + entityID);

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetEntityResponse>();
                   

                    entityList = readTask;
                    entityUpdateRequest.Id = entityList.Id;
                    entityUpdateRequest.ProjectID = entityList.ProjectID;
                    entityUpdateRequest.Title = entityList.Title;
                    entityUpdateRequest.Active = entityList.Active;
                    entityUpdateRequest.FileName = entityList.ThumbnailUrl;

                    entityUpdateRequest.DisplayName = entityList.DisplayName;
                    entityUpdateRequest.Argument1 = entityList.Argument1;
                    entityUpdateRequest.Argument2 = entityList.Argument2;
                    entityUpdateRequest.Argument3 = entityList.Argument3;

                }
                return entityUpdateRequest;
            }
            catch (Exception)
            {

                throw ;
            }
        }

        public async Task<GetEntityResponse> GetEntityByIdForDelete(string accessKey, Guid entityID)
        {
            try
            {
                GetEntityResponse entityList = new GetEntityResponse();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.GetAsync("Entity/1.0/" + entityID);

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetEntityResponse>();

                    entityList = readTask;
                }
                return entityList;
            }
            catch (Exception )
            {

                throw ;
            }
        }

        public async Task<IEnumerable<GetEntityResponse>> GetEntityList(string accessKey)
        {
            try
            {
                List<GetEntityResponse> entityLists = new List<GetEntityResponse>();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var response = await httpClient.GetAsync("Entity/1.0");

                if (response.IsSuccessStatusCode)
                {
                    var readTask = await response.Content.ReadAsAsync<List<GetEntityResponse>>();
                    entityLists = readTask;
                }
                return entityLists;
            }
            catch (Exception )
            {

                throw ;
            }
        }

        public async Task<int> UpdateEntity(string accessKey, EntityUpdateRequest entityUpdateRequest)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.PutAsJsonAsync("Entity/1.0/" + entityUpdateRequest.Id, entityUpdateRequest);
                var readTask = await responseTask.Content.ReadAsAsync<GetEntityResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.NoContent)
                {
                    status = 200;
                }
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
