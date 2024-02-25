using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Service
{
    public interface IProjectUserService
    {
        Task<IEnumerable<GetProjectUserResponse>> GetAll();
        Task<ProjectUser> AddProjectUser(ProjectUserAddRequest projectUserAddRequest);
        Task<ProjectUser> UpdateProjectUser(ProjectUserUpdateRequest projectUserUpdateRequest);
        Task<ProjectUser> RemoveProjectUser(Guid id);
        Task<GetProjectUserResponse> GetByID(Guid ID);
        Task<bool> CheckCombination(Guid UserId, Guid ProjectId);


    }
}
