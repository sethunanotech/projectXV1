using ProjectX.Application.Usecases.Entity;
using ProjectX.Application.Usecases.Package;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Service
{
   public interface IEntityService
    {
        Task<IEnumerable<GetEntityResponse>> GetAll();
        Task<Entity> AddEntity(EntityAddRequest entityAddRequest);
        Task<GetEntityResponse> GetByID(Guid ID);
        Task<Entity> RemoveEntity(Guid id);
        Task<Entity> UpdateEntity(EntityUpdateRequest entityUpdateRequest);

    }
}
