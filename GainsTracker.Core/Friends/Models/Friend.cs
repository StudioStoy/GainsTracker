using GainsTracker.Core.Components.Workouts.Models;

namespace GainsTracker.Core.Components.Friends.Models;

public class Friend
{
    public Friend(string name, string handle, Guid gainsId, DateTime friendsSince)
    {
        FriendName = name;
        FriendHandle = handle;
        GainsAccountId = gainsId;
        FriendsSince = friendsSince;
    }

    public Friend(GainsAccount account, DateTime friendsSince)
    {
        FriendName = !string.IsNullOrEmpty(account.UserProfile.DisplayName) ? account.UserProfile.DisplayName : account.UserHandle;
        FriendHandle = account.UserHandle;
        GainsAccountId = account.Id;
        FriendsSince = friendsSince;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GainsAccountId { get; set; }
    public DateTime FriendsSince { get; set; }
    public string FriendName { get; set; }
    public string FriendHandle { get; set; }
}
