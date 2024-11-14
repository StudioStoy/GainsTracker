using System.Linq.Expressions;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Workouts.Models;

namespace GainsTracker.Core;

public interface IBigBrain<T> where T : class
{
    Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? includes = null);
    Task<T?> GetByIdAsync(string id, Func<IQueryable<T>, IQueryable<T>>? includes = null);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null);
    Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    T Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    bool Exists(Expression<Func<T, bool>> predicate);

    Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle);
    Task<Guid> GetGainsIdByUsername(string userHandle);
    Task SaveContext();
    
}
