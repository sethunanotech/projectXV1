using ProjectX.Application.Usecases.DropDown;

namespace ProjectX.Application.Service
{
    public interface IDropDownService
    {
        Task<List<DropDownModel>> GetClientDropdownList();
        Task<List<DropDownModel>> GetProjectDropdownList();
        Task<List<DropDownModel>> GetUserDropdownList();
        Task<List<DropDownModel>> CheckProjectUserDropdown(Guid projectId);
           
    }
}
