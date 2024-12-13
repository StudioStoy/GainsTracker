#region

using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace GainsTracker.Data.Gains;

// TODO: Refactor database repositories to return nullable entities. Null logic should be decided in the services.
public class GainsRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<GainsAccount>(contextFactory), IGainsRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;
    private readonly GainsDbContext _sharedContext = contextFactory.CreateDbContext();

    public async Task<GainsAccount> GetGainsAccountByUserHandle(string userHandle)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gains = await context.GainsAccounts
            .FirstOrDefaultAsync(gains => gains.UserHandle.ToLower() == userHandle.ToLower());

        if (gains == null)
            throw new NotFoundException("Gains account not found with that userHandle");

        return gains;
    }

    public async Task<GainsAccount> GetGainsAccountWithRelationsByUserHandle(string userHandle)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gains = await context.GainsAccounts
            .Include(g => g.UserProfile)
            .ThenInclude(u => u!.Icon)
            .FirstOrDefaultAsync(gains => gains.UserHandle == userHandle);

        if (gains == null)
            throw new NotFoundException("Gains account not found with that userHandle");

        return gains;
    }
}
