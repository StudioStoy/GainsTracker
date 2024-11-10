using System.Linq.Expressions;
using GainsTracker.Common.Exceptions;
using GainsTracker.Core;
using GainsTracker.Core.Workouts.Models;
using GainsTracker.Data.Gains;
using GainsTracker.Data.Gains.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Shared;

/// <summary>
///     Oh yeah, this is big brain time
/// </summary>
/// <remarks>
///     This is the generic repository class, which contains the basic CRUD actions for the given entity T.
/// </remarks>
public class BigBrain<T> : IBigBrain<T> where T : class
{
    private readonly GainsDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BigBrain(GainsDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? includes = null)
    {
        return await _dbSet.AsQueryable().ApplyIncludes(includes).ToListAsync();
    }

    // Uses the "Id" property on the entity.
    public async Task<T?> GetByIdAsync(string id, Func<IQueryable<T>, IQueryable<T>>? includes = null)
    {
        var query = _dbSet.AsQueryable().ApplyIncludes(includes);
        return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null)
    {
        var query = _dbSet.Where(predicate).ApplyIncludes(includes);
        return await query.ToListAsync();
    }

    public async Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null)
    {
        var query = _dbSet.Where(predicate).ApplyIncludes(includes);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public T Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        _context.SaveChanges();
    }

    public bool Exists(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.AsQueryable().Any(predicate);
    }

    public async Task SaveContext() => await _context.SaveChangesAsync();
    
    # region Resuable GainsAccount queries
    public async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        GainsAccountEntity? gains = await _context.GainsAccounts.FirstOrDefaultAsync(gains =>
            string.Equals(gains.UserHandle.ToLower(), userHandle.ToLower()));
        
        if (gains == null)
            throw new NotFoundException("Gains account not found with that userHandle");

        return gains.MapToModel();
    }

    public async Task<Guid> GetGainsIdByUsername(string userHandle)
    {
        var idModel =  await _context.GainsAccounts.Where(g => g.UserHandle == userHandle)
            .Select(g => new { g.Id })
            .FirstOrDefaultAsync();
        
        if (idModel == null)
            throw new NotFoundException("User not found");

        return idModel.Id;
    }
    #endregion
}
