using System.Linq.Expressions;
using GainsTracker.Common.Exceptions;
using GainsTracker.Core;
using GainsTracker.Core.Gains.Models;
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

    public async Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
    {
        await using var context = contextFactory.CreateDbContext();
        var query = context.Set<TEntity>().Where(predicate).ApplyIncludes(includes);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
    {
        await using var context = contextFactory.CreateDbContext();
        var query = context.Set<TEntity>().Where(predicate).ApplyIncludes(includes);
        return await query.ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
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
    
    public async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        await using var context = contextFactory.CreateDbContext();
        var gainsAccount = await context.GainsAccounts
            .Where(g => g.UserHandle == userHandle)
            .Include(g => g.UserProfile)
            .ThenInclude(u => u.Icon)
            .FirstOrDefaultAsync();

        if (gainsAccount == null)
            throw new NotFoundException("User not found");

        return gainsAccount;
    }
}
