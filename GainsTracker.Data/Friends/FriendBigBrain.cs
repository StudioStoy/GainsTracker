using GainsTracker.Common.Exceptions;
using GainsTracker.Core.Components.Friends;
using GainsTracker.Core.Components.Friends.Interfaces.Repositories;
using GainsTracker.Core.Components.Friends.Models;
using GainsTracker.Core.Components.Workouts.Models;
using GainsTracker.Data.Gains;
using GainsTracker.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Friends;

public class FriendBigBrain(GainsDbContext context) : BigBrain<Friend>(context), IFriendBigBrain
{
    private readonly GainsDbContext _context = context;

    public async Task<List<Friend>> GetFriendsByGainsId(Guid gainsId)
    {
        var gainsWithFriends = await _context.GainsAccounts
            .Include(g => g.Friends)
            .FirstOrDefaultAsync(g => g.Id == gainsId);

        if (gainsWithFriends == null)
            return [];
        
        return gainsWithFriends.Friends.Select(f => f.MapToModel()).ToList();
    }

    public async Task<GainsAccount> GetFriendInfoByGainsId(Guid gainsId)
    {
        var gains = await _context.GainsAccounts
                   .Include(g => g.SentFriendRequests)
                   .ThenInclude(req => req.Recipient)
                   .AsSplitQuery()
                   .Include(g => g.ReceivedFriendRequests)
                   .ThenInclude(req => req.Requester)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync(g => g.Id == gainsId);
        
        if (gains == null)    
            throw new NotFoundException($"User with id {gainsId} was not found.");

        return gains.MapToModel();
    }

    public async Task<FriendRequest> GetFriendRequestById(Guid requestId)
    {
        FriendRequestEntity? request = await _context.FriendRequests
            .Include(req => req.Requester)
            .Include(req => req.Recipient)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (request == null)
            throw new NotFoundException("Request not found.");

        return request.MapToModel();
    }
}
