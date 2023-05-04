using GainsTrackerAPI.Components.Gains.Models;

namespace GainsTrackerAPI.Components.Friends.Models;

public class Friend
{
    // EF Core constructor.
    private Friend()
    {
    }

    public Friend(string name, string gainsId, DateTime friendsSince)
    {
        Name = name;
        GainsAccountId = gainsId;
        FriendsSince = friendsSince;
    }

    public Friend(GainsAccount account, DateTime friendsSince)
    {
        Name = account.Username;
        GainsAccountId = account.Id;
        FriendsSince = friendsSince;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }
    public DateTime FriendsSince { get; set; }
    public string Name { get; set; }
}
