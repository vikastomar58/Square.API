using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Squares.API.DataLayer.Core.Repository
{
    public interface IEfRepository<TEntity> where TEntity:class
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task AddAsync(IEnumerable<TEntity> entities);

        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task Delete(TEntity entity);
    }
}
