using ProjectX.Application.Usecases.VersionManagement;

namespace ProjectX.Application.Service
{
    public interface IVersionManagementService
    {
        Task<List<GetUpdateResponse>> GetUpdatedPackageList(GetUpdatesRequest getUpdatesRequest);
    }
}
