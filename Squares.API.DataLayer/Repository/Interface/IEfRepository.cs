using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Squares.API.DataLayer.Core.Repository
{
    public interface IEfRepository<TEntity> where TEntity : class
    {
        

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        IQueryable<TEntity> FindAllAsync();

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task Delete(TEntity entity);
    }
}
