namespace GainsTracker.Data.Friends;

public class Friend
{
    private Friend() {}
    
    public string Id { get; set; } = string.Empty;
    public string GainsAccountId { get; set; } = string.Empty;
    public DateTime FriendsSince { get; set; }
    public string FriendName { get; set; } = string.Empty;
    public string FriendHandle { get; set; } = string.Empty;
}