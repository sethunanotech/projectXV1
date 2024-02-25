using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.VersionManagement;

namespace ProjectX.Application.Service
{
    public class VersionManagementService: IVersionManagementService
    {
        private readonly IPackage _packageRepository;
        public VersionManagementService(IPackage packageRepository)
        {
            _packageRepository = packageRepository;
        }
        public async Task<List<GetUpdateResponse>> GetUpdatedPackageList(GetUpdatesRequest getUpdatesRequest)
        {
            List<GetUpdateResponse> getUpdateResponses = new List<GetUpdateResponse>();
            var packageList =await _packageRepository.GetUpdatedPackageList(getUpdatesRequest);
            if(packageList.Count >0)
            { 
                foreach (var package in packageList)
                {
                    GetUpdateResponse getUpdate = new GetUpdateResponse();
                    getUpdate.Version = package.Version;
                    getUpdate.Url = package.Url;
                    getUpdateResponses.Add(getUpdate);
                }
                
            }
            return getUpdateResponses;
        }
    }
}
