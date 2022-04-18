using Microsoft.EntityFrameworkCore;
using Squares.API.DataLayer.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Squares.API.DataLayer.Core.Repository
{
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : class
    {
      
        protected readonly DbSet<TEntity> Set;

        public DbContext dbContext { get ; set ; }

        public EfRepository(ISquareUow squareUOW)
        {
            dbContext = squareUOW.dbContext;
            Set = this.dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Method for Adding an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Method for adding multiple entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbContext.Set<TEntity>().AddRangeAsync(entities);

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Method for returning entity as quertable
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> FindAllAsync()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// Method for finding single record
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Method for deleting a record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
