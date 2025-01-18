using System.Linq.Expressions;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);

    Task<Guid> GetGainsIdByUserHandle(string userHandle);
    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
}
