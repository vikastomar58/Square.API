using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Squares.API.DataLayer.Core.Repository
{
    public class EfRepository<TEntity>:IEfRepository<TEntity> where TEntity:class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> Set;

        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            Set = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<List<TEntity>>().FindAsync(predicate);
        }

        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FindAsync(predicate);
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
