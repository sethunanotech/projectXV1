using ProjectX.Application.Usecases.Package;

namespace ProjectX.WebApplication.IService
{
    public interface IPackageService
    {
        Task<IEnumerable<GetPackageResponse>> GetPackageList(string accessKey);
        Task<int> AddPackage(string accessKey, PackageAddRequest addPackageRequest);
        Task<int> UpdatePackage(string accessKey, PackageUpdateRequest updatePackageRequest);
        Task<int> DeletePackage(string accessKey, Guid packageID);
        Task<PackageUpdateRequest> GetPackageById(string accessKey, Guid packageID);
        Task<GetPackageResponse> GetPackageByIdForDelete(string accessKey, Guid packageID);
    }
}
