using GainsTracker.Common.Exceptions;
using GainsTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data;

public class GenericRepository<TEntity>(GainsDbContextFactory contextFactory) : IGenericRepository<TEntity>
    where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        await using var context = contextFactory.CreateDbContext();
        var entity = await context.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public async Task<List<TEntity>> GetAll()
    {
        await using var context = contextFactory.CreateDbContext();

        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await using var context = contextFactory.CreateDbContext();
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await using var context = contextFactory.CreateDbContext();
        var tracked = context.Update(entity);
        await context.SaveChangesAsync();
        return tracked.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var context = contextFactory.CreateDbContext();
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Guid> GetGainsIdByUserHandle(string userHandle)
    {
        await using var context = contextFactory.CreateDbContext();
        var idModel = await context.GainsAccounts
            .Where(g => g.UserHandle == userHandle)
            .Select(g => new { g.Id })
            .FirstOrDefaultAsync();

        if (idModel == null)
            throw new NotFoundException("User not found");

        return idModel.Id;
    }
}
