using ProjectX.Application.Usecases.VersionManagement;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Contracts
{
    public interface IPackage : IGenericRepository<Package>
    {
        Task<List<Package>> GetUpdatedPackageList(GetUpdatesRequest getUpdatesRequest);
        Task<bool> CheckCombinationprojectEntityExist(Guid entityID, Guid projectID);
    }
}
