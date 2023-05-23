using GainsTracker.CoreAPI.Components.Gains.Models;

namespace GainsTracker.CoreAPI.Components.Friends.Models;

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
    public string GainsAccountId { get; set; } = string.Empty;
    public DateTime FriendsSince { get; set; }
    public string FriendName { get; set; } = string.Empty;
    public string FriendHandle { get; set; } = string.Empty;
}
