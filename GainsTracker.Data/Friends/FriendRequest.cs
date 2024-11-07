using GainsTracker.Common.Models.Friends;

namespace GainsTracker.Data.Friends;

public class FriendRequest
{
    // EF Core constructor.
    private FriendRequest() { }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RequesterId { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;

    public GainsAccount Requester { get; set; } = null!;
    public GainsAccount Recipient { get; set; } = null!;

    public DateTime RequestTime { get; private set; }
    public FriendRequestStatus Status { get; set; }

    public bool Accepted => Status == FriendRequestStatus.Accepted;
}
