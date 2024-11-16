using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Friends.Models;

public class Friend(string name, string handle, Guid gainsId, DateTime friendsSince)
{
    public Friend(GainsAccount account, DateTime friendsSince)
        : this(
            !string.IsNullOrEmpty(account.UserProfile?.DisplayName)
                ? account.UserProfile.DisplayName
                : account.UserHandle,
            account.UserHandle,
            account.Id,
            friendsSince
        ) { }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GainsAccountId { get; } = gainsId;
    public DateTime FriendsSince { get; } = friendsSince;
    public string FriendName { get; } = name;
    public string FriendHandle { get; } = handle;
}