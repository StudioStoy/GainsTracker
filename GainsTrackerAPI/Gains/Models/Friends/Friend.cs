namespace GainsTrackerAPI.Gains.Models.Friends;

public class Friend
{
    // EF Core constructor.
    private Friend()
    {
    }

    public Friend(string name, string gainsId)
    {
        Name = name;
        GainsAccountId = gainsId;
    }

    public Friend(GainsAccount account)
    {
        Name = account.Username;
        GainsAccountId = account.Id;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }
    public string Name { get; set; }
}
