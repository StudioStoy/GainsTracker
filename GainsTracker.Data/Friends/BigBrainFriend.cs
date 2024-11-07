using GainsTracker.Common.Exceptions;

namespace GainsTracker.Data.Friends;

public class BigBrainFriend : BigBrain
{
    public BigBrainFriend(AppDbContext context) : base(context)
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
                   .ThenInclude(req => req.Recipient)
                   .AsSplitQuery()
                   .Include(g => g.ReceivedFriendRequests)
                   .ThenInclude(req => req.Requester)
                   .AsSplitQuery()
                   .FirstOrDefault(g => g.Id == gainsId)
               ?? throw new NotFoundException($"User with id {gainsId} was not found.");
    }

    public FriendRequest GetFriendRequestById(string requestId)
    {
        return Context.FriendRequests
                   .Include(req => req.Requester)
                   .Include(req => req.Recipient)
                   .FirstOrDefault(r => r.Id == requestId)
               ?? throw new NotFoundException("Request not found.");
    }
}
