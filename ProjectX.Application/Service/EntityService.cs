using AutoMapper;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Application.Usecases.Package;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;

namespace ProjectX.Application.Service
{
    public class EntityService : IEntityService
    {
        private readonly IEntity _entityRepository;
        private readonly IMapper _mapper;
        private readonly ICryptography _cryptography;
        private readonly int defaultValue;
        public EntityService(IEntity entityRepositoryy, ICryptography cryptography, IMapper mapper)
        {
            _entityRepository = entityRepositoryy;
            _cryptography = cryptography;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetEntityResponse>> GetAll()
        {           
            List<GetEntityResponse> listOfEntities = new List<GetEntityResponse>();
            var entity = await _entityRepository.GetAllAsync();
            if (entity.Any())
            {
                listOfEntities = _mapper.Map<List<GetEntityResponse>>(entity);
            }            
            return listOfEntities;
        }

        public async Task<Entity> AddEntity(EntityAddRequest entityAddRequest)
        {
            Entity entity = null;
            var entityData = _mapper.Map<Entity>(entityAddRequest);
            var packageUrl = await _cryptography.SaveFile(entityAddRequest.File, defaultValue, "");
            if (packageUrl != "")
            {
                entityData.ThumbnailUrl = packageUrl;
                entity = await _entityRepository.AddAsync(entityData);
            }
            return entity;
        }

        public async Task<GetEntityResponse> GetByID(Guid ID)
        {
            var entity = await _entityRepository.GetByIdAsync(ID);
            var getEntityById = _mapper.Map<GetEntityResponse>(entity);
            return getEntityById;
        }

        public async Task<Entity> RemoveEntity(Guid Id)
        {
            Entity entity = new Entity();
            entity = await _entityRepository.GetByIdAsync(Id);
            if (entity != null)
            {
                await _entityRepository.RemoveByIdAsync(entity.Id);
            }
            return entity;
        }

        public async Task<Entity> UpdateEntity(EntityUpdateRequest entityUpdateRequest)
        {
            Entity updateEntity = new Entity();
            var entity = _mapper.Map<Entity>(entityUpdateRequest);
            var existingEntity = await _entityRepository.GetByIdAsync(entityUpdateRequest.Id);
            if (existingEntity != null)
            {
                entity.CreatedOn = existingEntity.CreatedOn;
                entity.CreatedBy = existingEntity.CreatedBy;
                var entityUrl = await _cryptography.SaveFile(entityUpdateRequest.File, defaultValue, "");
                if (entityUrl != "")
                {
                    entity.ThumbnailUrl = entityUrl;
                    updateEntity = await _entityRepository.UpdateAsync(entity);
                }
            }
            return updateEntity;
        }
    }
}
