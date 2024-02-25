using ProjectX.Application.Usecases.Package;
using ProjectX.Application.Usecases.User;
using ProjectX.WebApplication.IService;
using System.Net;

namespace ProjectX.WebApplication.Service
{
    public class PackageService: IPackageService
    {
        private readonly ILoginService _loginService;
        public PackageService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<IEnumerable<GetPackageResponse>> GetPackageList(string accessKey)
        {
            try
            {
                List<GetPackageResponse> packageLists = new List<GetPackageResponse>();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var response = await httpClient.GetAsync("Package/1.0");

                if (response.IsSuccessStatusCode)
                {
                    var readTask = await response.Content.ReadAsAsync<List<GetPackageResponse>>();
                    packageLists = readTask;
                }
                return packageLists;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<int> AddPackage(string accessKey, PackageAddRequest addPackageRequest)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.PostAsJsonAsync("Package/1.0", addPackageRequest);
                var readTask = await responseTask.Content.ReadAsAsync<GetPackageResponse>();
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

        public async Task<int> DeletePackage(string accessKey, Guid packageID)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.DeleteAsync("Package/1.0/" + packageID);
                var readTask = await responseTask.Content.ReadAsAsync<GetPackageResponse>();
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

        public async Task<PackageUpdateRequest> GetPackageById(string accessKey, Guid packageID)
        {
            try
            {
                GetPackageResponse packageList = new GetPackageResponse();
                PackageUpdateRequest updatePackageRequest = new PackageUpdateRequest();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.GetAsync("Package/1.0/" + packageID);

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetPackageResponse>();

                    packageList = readTask;
                    updatePackageRequest.Id = packageList.ID;
                    updatePackageRequest.ProjectID = packageList.ProjectID;
                    updatePackageRequest.Version = packageList.Version;
                    updatePackageRequest.Active = packageList.Active;
                }
                return updatePackageRequest;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<int> UpdatePackage(string accessKey, PackageUpdateRequest updatePackageRequest)
        {
            try
            {
                int status = 400;
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.PutAsJsonAsync("Package/1.0/"+ updatePackageRequest.Id, updatePackageRequest);
                var readTask = await responseTask.Content.ReadAsAsync<GetPackageResponse>();
                var statusCode = responseTask.StatusCode;
                if (statusCode == HttpStatusCode.NoContent)
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

        public async Task<GetPackageResponse> GetPackageByIdForDelete(string accessKey, Guid packageID)
        {
            try
            {
                GetPackageResponse packageList = new GetPackageResponse();
                var httpClient = _loginService.ToaccesAPI(accessKey);
                var responseTask = await httpClient.GetAsync("Package/1.0/" + packageID);

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetPackageResponse>();

                    packageList = readTask;
                }
                return packageList;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
