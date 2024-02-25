using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Service
{
    public interface IUserService
    {
        Task<User> AddUser(UserAddRequest userAddRequest);
        Task<User> UpdateUser(UserUpdateRequest userUpdateRequest);
        Task<User> RemoveUser(Guid Id);
        Task<IEnumerable<GetUserResponse>> GetAll();
        Task<GetUserResponse> GetByID(Guid ID);
    }
}
