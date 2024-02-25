using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Application.Usecases.Package;

namespace ProjectX.WebApplication.IService
{
    public interface IEntityService
    {
        Task<IEnumerable<GetEntityResponse>> GetEntityList(string accessKey);
        Task<int> AddEntity(string accessKey, EntityAddRequest entityAddRequest, IFormFile imageFile);
        Task<int> UpdateEntity(string accessKey, EntityUpdateRequest entityUpdateRequest);
        Task<int> DeleteEntity(string accessKey, Guid packageID);
        Task<EntityUpdateRequest> GetEntityById(string accessKey, Guid entityID);
        Task<GetEntityResponse> GetEntityByIdForDelete(string accessKey, Guid entityID);
    }
}
