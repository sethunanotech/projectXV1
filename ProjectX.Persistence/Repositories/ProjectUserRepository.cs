using Microsoft.EntityFrameworkCore;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Data;

namespace ProjectX.Persistence.Repositories
{
    public class ProjectUserRepository : GenericRepository<ProjectUser>, IProjectUser
    {
        private readonly ApplicationDbContext _dbContext;
        public ProjectUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CheckCombinationExist(Guid userID, Guid projectID)
        {
            bool status = false;
            var projectUserext = await (from project in _dbContext.Projects
                                        join user in _dbContext.Users
                                        on project.ClientID equals user.ClientID
                                        where user.Id == userID && project.Id == projectID
                                        select project).FirstOrDefaultAsync();

            if (projectUserext != null)
            {
                status = true;
            }
            return status;
        }
        public async Task<List<DropDownModel>> CheckProjectUserExist(Guid projectId)
        {
            var projectUserExt = await (from project in _dbContext.Projects
                                        join user in _dbContext.Users
                                        on project.ClientID equals user.ClientID
                                        where project.Id == projectId
                                        select new DropDownModel { Id = user.Id, Name = user.Name }).ToListAsync();
            return projectUserExt;
        }
    }
}
