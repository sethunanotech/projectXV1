using ProjectX.Application.Usecases.DropDown;
using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Contracts
{
    public interface IUser : IGenericRepository<User>
    {
     
        Task<List<DropDownModel>> UserList();
        Task<User> IsUserNamePasswordExist(User user);
    }
}
