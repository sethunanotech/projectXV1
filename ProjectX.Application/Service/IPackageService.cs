using ProjectX.Application.Usecases.Package;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Service
{
    public interface IPackageService
    {
        Task<IEnumerable<GetPackageResponse>> GetAll();
        Task<Package> AddPackage(PackageAddRequest packageAddRequest);
        Task<Package> UpdatePackage(PackageUpdateRequest packageUpdateRequest);
        Task<Package> RemovePackage(Guid id);
        Task<GetPackageResponse> GetByID(Guid ID);
        Task<bool> CheckCombinationprojectEntityExist(Guid entityID, Guid projectID);
    }
}
