using GainsTracker.Common.Exceptions;
using GainsTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data;

public class GenericRepository<TDomain, TEntity>(GainsDbContextFactory contextFactory) : IGenericRepository<TDomain>
    where TDomain : class
    where TEntity : class
{
    public async Task<TDomain?> GetByIdAsync(Guid id)
    {
        await using var context = contextFactory.CreateDbContext([]);
        var entity = await context.Set<TEntity>().FindAsync(id);
        return entity?.AsDomain<TDomain, TEntity>();
    }

    public IQueryable<TDomain> GetAll()
    {
        var context = contextFactory.CreateDbContext([]);

        return context.Set<TEntity>()
            .Select(entity => entity.AsDomain<TDomain, TEntity>());
    }

    public async Task AddAsync(TDomain domain)
    {
        await using var context = contextFactory.CreateDbContext([]);
        var entity = domain.AsEntity<TDomain, TEntity>();
        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public void Update(TDomain domain)
    {
        using var context = contextFactory.CreateDbContext([]);
        var entity = domain.AsEntity<TDomain, TEntity>();

        context.Set<TEntity>().Update(entity);

        context.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var context = contextFactory.CreateDbContext([]);
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Guid> GetGainsIdByUserHandle(string userHandle)
    {
        await using var context = contextFactory.CreateDbContext([]);
        var idModel = await context.GainsAccounts
            .Where(g => g.UserHandle == userHandle)
            .Select(g => new { g.Id })
            .FirstOrDefaultAsync();

        if (idModel == null)
            throw new NotFoundException("User not found");

        return idModel.Id;
    }
}
