namespace GainsTracker.Core;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAll();
    Task AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);

    Task<Guid> GetGainsIdByUserHandle(string username);
}
