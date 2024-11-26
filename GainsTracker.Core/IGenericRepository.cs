namespace GainsTracker.Core;

public interface IGenericRepository<TDomain> where TDomain : class
{
    Task<TDomain?> GetByIdAsync(Guid id);
    IQueryable<TDomain> GetAll();
    Task AddAsync(TDomain entity);
    void Update(TDomain entity);
    Task DeleteAsync(Guid id);

    Task<Guid> GetGainsIdByUserHandle(string username);
}
