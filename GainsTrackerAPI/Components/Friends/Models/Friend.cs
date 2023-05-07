using GainsTrackerAPI.Components.Gains.Models;

namespace GainsTrackerAPI.Components.Friends.Models;

public class Friend
{
    // EF Core constructor.
    private Friend()
    {
    }

    public Friend(string name, string handle, string gainsId, DateTime friendsSince)
    {
        FriendName = name;
        FriendHandle = handle;
        GainsAccountId = gainsId;
        FriendsSince = friendsSince;
    }

    public Friend(GainsAccount account, DateTime friendsSince)
    {
        FriendName = !string.IsNullOrEmpty(account.DisplayName) ? account.DisplayName : account.UserHandle;
        FriendHandle = account.UserHandle;
        GainsAccountId = account.Id;
        FriendsSince = friendsSince;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }
    public DateTime FriendsSince { get; set; }
    public string FriendName { get; set; }
    public string FriendHandle { get; set; }
}
