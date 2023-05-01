namespace GainsTrackerAPI.Gains.Models.Friends;

public class Friend
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
}
