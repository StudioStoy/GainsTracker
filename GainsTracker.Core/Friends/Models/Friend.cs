#region

using GainsTracker.Core.Gains.Models;

#endregion

namespace GainsTracker.Core.Friends.Models;

public class Friend
{
    public Friend()
    {
    }

    public Friend(string name, string handle, DateTime friendsSince)
    {
        FriendName = name;
        FriendHandle = handle;
        FriendsSince = friendsSince;
    }

    public Friend(GainsAccount account, DateTime friendsSince) : this
    (
        !string.IsNullOrEmpty(account.UserProfile?.DisplayName)
            ? account.UserProfile.DisplayName
            : account.UserHandle,
        account.UserHandle,
        friendsSince
    )
    {
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime FriendsSince { get; init; }
    public string FriendName { get; init; } = string.Empty;
    public string FriendHandle { get; init; } = string.Empty;
}
