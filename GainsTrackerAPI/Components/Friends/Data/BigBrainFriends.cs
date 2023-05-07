using GainsTrackerAPI.Components.Friends.Models;
using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Configurations.Database;
using GainsTrackerCommon.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Components.Friends.Data;

public class BigBrainFriends : BigBrain
{
    public BigBrainFriends(AppDbContext context) : base(context)
    {
    }

    public List<Friend> GetFriendsByGainsId(string gainsId)
    {
        List<Friend>? friendsByGainsId = Context.GainsAccounts
            .Include(g => g.Friends)
            .FirstOrDefault(g => g.Id == gainsId)?.Friends;
        return friendsByGainsId ?? new List<Friend>();
    }

    public GainsAccount GetFriendInfoByGainsId(string gainsId)
    {
        return Context.GainsAccounts
                   .Include(g => g.SentFriendRequests)
                   .ThenInclude(req => req.RequestedTo)
                   .AsSplitQuery()
                   .Include(g => g.ReceivedFriendRequests)
                   .ThenInclude(req => req.RequestedBy)
                   .AsSplitQuery()
                   .FirstOrDefault(g => g.Id == gainsId)
               ?? throw new NotFoundException($"User with id {gainsId} was not found.");
    }

    public FriendRequest GetFriendRequestById(string requestId)
    {
        return Context.FriendRequests
                   .Include(req => req.RequestedBy)
                   .Include(req => req.RequestedTo)
                   .FirstOrDefault(r => r.Id == requestId)
               ?? throw new NotFoundException("Request not found.");
    }
}
