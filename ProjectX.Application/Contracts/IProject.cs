using ProjectX.Application.Usecases.DropDown;
using ProjectX.Application.Usecases.Login;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Contracts
{
    public interface IProject :IGenericRepository<Project>
    {
        Task<List<DropDownModel>> ProjectList();
        Task<Project> CheckClientIdSecretCodeExist(ClientLogin clientLogin);
    }
}
