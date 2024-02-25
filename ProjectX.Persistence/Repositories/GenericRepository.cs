using Microsoft.EntityFrameworkCore;
using ProjectX.Application.Contracts;
using ProjectX.Persistence.Data;
using System.Linq.Expressions;

namespace ProjectX.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            var list = await _dbContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                    .ToListAsync();
            return list;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var list = await _dbContext.Set<T>()
                    .AsNoTracking().ToListAsync();
            return list;
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>()
                        .AsNoTracking()
                       .FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == id);
        }
        public async void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(Guid Id)
        {
            var _entity = await _dbContext.Set<T>().FindAsync(Id);
            if (_entity != null)
            {
                _dbContext.Set<T>().Remove(_entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
      

    }
}
