using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Models;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Gains;

// TODO: Refactor database repositories to return nullable entities. Null logic should be decided in the services.
public class GainsRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<GainsAccount>(contextFactory), IGainsRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gains = await context.GainsAccounts
            .Include(g => g.UserProfile)
            .ThenInclude(u => u!.Icon)
            .FirstOrDefaultAsync(gains => gains.UserHandle == userHandle) 
                    ?? throw new NotFoundException("Gains account not found with that userHandle");
        
        return gains;
    }

    public async Task<GainsAccount> CreateGainsAccountForUser(GainsAccount gainsAccount)
    {
        await using var context = _contextFactory.CreateDbContext();
        return context.GainsAccounts.Add(gainsAccount).Entity;
    }
}
