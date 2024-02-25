using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Contracts
{
    public interface IProjectUser: IGenericRepository<ProjectUser>
    {
        Task<bool> CheckCombinationExist(Guid userID, Guid projectID);
        Task<List<DropDownModel>> CheckProjectUserExist(Guid projectId);
    }
}
