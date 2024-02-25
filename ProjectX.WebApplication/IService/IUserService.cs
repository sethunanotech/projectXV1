using ProjectX.Application.Usecases.User;

namespace ProjectX.WebApplication.IService
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserResponse>> GetUserList(string accessKey);
        Task<int> AddUser(string accessKey, UserAddRequest userRequest);
        Task<int> UpdateUser(string accessKey, UserUpdateRequest updateUserRequest);
        Task<UserUpdateRequest> GetUserById(string accessKey, Guid userId);
        Task<int> DeleteUser(string accessKey, Guid UserId);
        Task<GetUserResponse> DeleteById(string accessKey, Guid userId);
    }
}
