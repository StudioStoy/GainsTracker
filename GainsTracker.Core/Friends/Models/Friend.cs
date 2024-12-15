using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Friends.Models;

public class Friend
{
    public Friend()
    {
    }

    public Friend(string name, string handle, DateTime friendsSince)
    {
        Name = name;
        Handle = handle;
        FriendsSince = friendsSince;
    }

    public Friend(GainsAccount account, DateTime friendsSince) : this
    (
        !string.IsNullOrEmpty(account.UserProfile.DisplayName)
            ? account.UserProfile.DisplayName
            : account.UserHandle,
        account.UserHandle,
        friendsSince
    )
    {
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime FriendsSince { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Handle { get; init; } = string.Empty;
}
