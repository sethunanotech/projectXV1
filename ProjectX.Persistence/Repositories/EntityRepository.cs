using ProjectX.Application.Contracts;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Data;

namespace ProjectX.Persistence.Repositories
{
    public class EntityRepository : GenericRepository<Entity>, IEntity
    {
        public EntityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
