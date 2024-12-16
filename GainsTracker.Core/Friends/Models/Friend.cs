using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Friends.Models;

public class Friend
{
    public Friend()
    {
    }

    public Friend(GainsAccount account, DateTime friendsSince)
    {
        Handle = account.UserHandle;
        FriendsSince = friendsSince;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime FriendsSince { get; init; }
    public string Handle { get; init; } = string.Empty;
}
