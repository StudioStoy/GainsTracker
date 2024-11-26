using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Data.Friends.Entities;
using GainsTracker.Data.Gains;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Friends;

public class FriendRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<Friend, FriendEntity>(contextFactory), IFriendRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task<List<Friend>> GetFriendsByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext([]);

        var gainsWithFriends = await context.GainsAccounts
            .Include(g => g.Friends)
            .FirstOrDefaultAsync(g => g.Id == gainsId);

        if (gainsWithFriends == null)
            return [];

        return gainsWithFriends.Friends.Select(f => f.ToModel()).ToList();
    }

    public async Task<GainsAccount> GetFriendInfoByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext([]);

        // Fetch the entity with all required relationships
        var gains = await context.GainsAccounts
            .Include(g => g.SentFriendRequests)
            .ThenInclude(req => req.Recipient)
            .AsSplitQuery()
            .Include(g => g.ReceivedFriendRequests)
            .ThenInclude(req => req.Requester)
            .AsSplitQuery()
            .FirstOrDefaultAsync(g => g.Id == gainsId);

        if (gains == null)
            throw new NotFoundException($"User with id {gainsId} was not found.");

        // Map the entity to the domain model
        return gains.ToModel();
    }


    public async Task<FriendRequest> GetFriendRequestById(Guid requestId)
    {
        await using var context = _contextFactory.CreateDbContext([]);

        var request = await context.FriendRequests
            .Include(req => req.Requester)
            .Include(req => req.Recipient)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (request == null)
            throw new NotFoundException("Request not found.");

        return request.ToModel();
    }
}
