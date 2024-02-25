using Microsoft.EntityFrameworkCore;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.DropDown;
using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Data;
using System.Security.Cryptography;
using System.Text;

namespace ProjectX.Persistence.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProject
    {
        private readonly ApplicationDbContext _dbContext;
        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
       
       
        public async Task<List<DropDownModel>> ProjectList()
        {
            var clientLists = await (from project in _dbContext.Projects
                                     select new
                                     DropDownModel
                                     { Id = project.Id, Name = project.Title }).
                                   ToListAsync();
            return clientLists;
        }

        public async Task<Project> CheckClientIdSecretCodeExist(ClientLogin clientLogin)
        {
            var project = await _dbContext.Projects.FirstOrDefaultAsync(x=>x.ClientID==clientLogin.ClientID && x.SecretCode==clientLogin.SecretCode &&x.Active==true);
            if (project != null)
            {
                return project;
            }
            return null;
        }
    }
}
