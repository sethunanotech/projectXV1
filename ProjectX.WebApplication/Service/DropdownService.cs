using ProjectX.Application.Usecases.DropDown;
using ProjectX.WebApplication.IService;

namespace ProjectX.WebApplication.Service
{
    public class DropdownService : IDropdownService
    {
        private readonly ILoginService _loginService;
        public DropdownService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<IEnumerable<DropDownModel>> GetClientDropdownList(string accessKey)
        {
            IEnumerable<DropDownModel> clientLists = null;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var response = await httpClient.GetAsync("DropDown/1.0/GetClientDropDown");
            if (response.IsSuccessStatusCode)
            {
                var readTask = await response.Content.ReadAsAsync<List<DropDownModel>>();
                clientLists = readTask;
            }
            return clientLists;
        }
        public async Task<IEnumerable<DropDownModel>> GetProjectDropdownList(string accessKey)
        {
            IEnumerable<DropDownModel> projectList = null;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var response = await httpClient.GetAsync("DropDown/1.0/GetProjectDropDown");
            if (response.IsSuccessStatusCode)
            {
                var readTask = await response.Content.ReadAsAsync<List<DropDownModel>>();
                projectList = readTask;
            }
            return projectList;
        }
        public async Task<IEnumerable<DropDownModel>> GetBindedUserDropDownList(string accessKey, Guid projectID)
        {
            IEnumerable<DropDownModel> userLists = null;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var response = await httpClient.GetAsync("DropDown/1.0/GetBindedUserDropDown?projectId=" + projectID);
            if (response.IsSuccessStatusCode)
            {
                var readTask = await response.Content.ReadAsAsync<List<DropDownModel>>();
                userLists = readTask;
            }
            return userLists;
        }
        public async Task<IEnumerable<DropDownModel>> GetUserDropDownList(string accessKey)
        {
            IEnumerable<DropDownModel> userList = null;
            var httpClient = _loginService.ToaccesAPI(accessKey);
            var response = await httpClient.GetAsync("DropDown/1.0/GetUserDropDown");
            if (response.IsSuccessStatusCode)
            {
                var readTask = await response.Content.ReadAsAsync<List<DropDownModel>>();
                userList = readTask;
            }
            return userList;
        }
    }
}
