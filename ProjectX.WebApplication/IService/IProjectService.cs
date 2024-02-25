using ProjectX.Application.Usecases.Projects;

namespace ProjectX.WebApplication.IService
{
    public interface IProjectService
    {
        Task<IEnumerable<GetProjectResponse>> GetProjectList(string accessKey);
        Task<int> AddProject(string accessKey, ProjectAddRequest addProjectRequest);
        Task<int> UpdateProject(string accessKey, ProjectUpdateRequest updateProjectRequest);
        Task<ProjectUpdateRequest> GetProjectById(string accessKey, Guid projectId);
        Task<GetProjectResponse> DeleteById(string accessKey, Guid projectId);
        Task<int> DeleteProject(string accessKey, Guid ProjectId);
    }
}
