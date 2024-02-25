using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Service
{
    public interface IProjectService
    {

        Task<IEnumerable<GetProjectResponse>> GetAll();
        Task<Project> AddProject(ProjectAddRequest projectAddRequest);
        Task<Project> UpdateProject(ProjectUpdateRequest projectUpdateRequest);
        Task<Project> RemoveProject(Guid id);
        Task<GetProjectResponse> GetByID(Guid ID);
    }
}
