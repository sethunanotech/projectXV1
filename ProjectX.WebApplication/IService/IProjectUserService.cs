using ProjectX.Application.Usecases.ProjectUsers;

namespace ProjectX.WebApplication.IService
{
    public interface IProjectUserService
    {
        Task<IEnumerable<GetProjectUserResponse>> GetProjectUserList(string accessKey);
        Task<int> AddProjectUser(string accessKey, ProjectUserAddRequest addProjectUserRequest);
        Task<int> UpdateProjectUser(string accessKey, ProjectUserUpdateRequest updateProjectUserRequest);
        Task<int> DeleteProjectUser(string accessKey, Guid projectUserID);
        Task<ProjectUserUpdateRequest> GetProjectUserById(string accessKey, Guid projectUserID);
    }
}
