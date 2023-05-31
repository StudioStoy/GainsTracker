using GainsTracker.Common.Exceptions;
using GainsTracker.CoreAPI.Components.Friend.Models;
using GainsTracker.CoreAPI.Components.Workout.Models;
using GainsTracker.CoreAPI.Configurations.Database;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.Friend.Data;

public class BigBrainFriend : BigBrain
{
    public BigBrainFriend(AppDbContext context) : base(context)
    {
    }

    public List<Models.Friend> GetFriendsByGainsId(string gainsId)
    {
        List<Models.Friend>? friendsByGainsId = Context.GainsAccounts
            .Include(g => g.Friends)
            .FirstOrDefault(g => g.Id == gainsId)?.Friends;
        return friendsByGainsId ?? new List<Models.Friend>();
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
