using Microsoft.EntityFrameworkCore;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.VersionManagement;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Data;

namespace ProjectX.Persistence.Repositories
{
    public class PackageRepository : GenericRepository<Package>, IPackage
    {
        private readonly ApplicationDbContext _dbContext;
        public PackageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<List<Package>>GetUpdatedPackageList(GetUpdatesRequest getUpdatesRequest)
        {
            var updatedVersion =await _dbContext.Packages.Where(x=>x.ProjectID == getUpdatesRequest.ProjectId).OrderBy(x => x.Version).ToListAsync();
           var updates= updatedVersion.FindAll(x=>x.Version>getUpdatesRequest.Version);
            return updates;
        }
        public async Task<bool> CheckCombinationprojectEntityExist(Guid entityID, Guid projectID)
        {
            bool status = false;
            var packageEntity = await (from p in _dbContext.Projects
                                       join e in _dbContext.Entities
                                       on p.Id equals e.ProjectID
                                       where e.Id == entityID && p.Id == projectID
                                       select p).FirstOrDefaultAsync();

            if (packageEntity != null)
            {
                status = true;
            }
            return status;
        }
    }
}
