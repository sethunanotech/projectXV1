using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Contracts
{
    public interface IClient : IGenericRepository<Client>
    {
        Task<List<DropDownModel>> ClientList();
    }
}
