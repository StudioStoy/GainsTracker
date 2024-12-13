#region

using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace GainsTracker.Data.Friends;

public class FriendRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<Friend>(contextFactory), IFriendRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;
    private readonly GainsDbContext _sharedContext = contextFactory.CreateDbContext();

    public async Task<List<Friend>> GetFriendsByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gainsWithFriends = await context.GainsAccounts
            .Include(g => g.Friends)
            .FirstOrDefaultAsync(g => g.Id == gainsId);

        if (gainsWithFriends == null)
            return [];

        return [.. gainsWithFriends.Friends];
    }

    public async Task<GainsAccount> GetFriendInfoByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

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

        return gains;
    }

    public async Task<FriendRequest> GetFriendRequestById(Guid requestId)
    {
        var request = await _sharedContext.FriendRequests
            .Include(req => req.Requester)
            .Include(req => req.Recipient)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (request == null)
            throw new NotFoundException("Request not found.");

        return request;
    }

    public async Task AddFriendRequest(FriendRequest friendRequest)
    {
        await using var context = _contextFactory.CreateDbContext();

        await context.FriendRequests.AddAsync(friendRequest);

        await context.SaveChangesAsync();
    }

    public async Task UpdateFriendRequest(FriendRequest friendRequest)
    {
        if (friendRequest.IsAccepted)
        {
            await _sharedContext.Friends.AddAsync(friendRequest.Requester.Friends[0]);
            await _sharedContext.Friends.AddAsync(friendRequest.Recipient.Friends[0]);
        }

        _sharedContext.FriendRequests.Update(friendRequest);

        await _sharedContext.SaveChangesAsync();
    }
}
