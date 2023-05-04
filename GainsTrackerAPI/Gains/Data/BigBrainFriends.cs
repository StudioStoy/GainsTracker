using GainsTrackerAPI.Db;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerLib.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Gains.Data;

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
                   .FirstOrDefault(r => r.Id == requestId)
               ?? throw new NotFoundException("Request not found.");
    }
}
