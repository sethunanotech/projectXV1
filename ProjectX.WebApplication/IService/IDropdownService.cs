using ProjectX.Application.Usecases.DropDown;

namespace ProjectX.WebApplication.IService
{
    public interface IDropdownService
    {
        Task<IEnumerable<DropDownModel>> GetClientDropdownList(string accessKey);
        Task<IEnumerable<DropDownModel>> GetProjectDropdownList(string accessKey);
        Task<IEnumerable<DropDownModel>> GetUserDropDownList(string accessKey);
        Task<IEnumerable<DropDownModel>> GetBindedUserDropDownList(string accessKey, Guid projectID);
    }
}
