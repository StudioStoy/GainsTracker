namespace GainsTrackerAPI.Gains.Models.Friends;

public class FriendRequest
{
    private FriendRequest()
    {
    } // EF Core constructor

    public FriendRequest(GainsAccount requestedBy, GainsAccount requestedTo)
    {
        RequestedBy = requestedBy;
        RequestedTo = requestedTo;
        RequestedById = requestedBy.Id;
        RequestedToId = requestedTo.Id;

        Status = FriendRequestStatus.Pending;
        RequestTime = DateTime.UtcNow;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RequestedById { get; set; }
    public string RequestedToId { get; set; }

    public GainsAccount RequestedBy { get; private set; }
    public GainsAccount RequestedTo { get; private set; }

    public DateTime RequestTime { get; private set; }
    public FriendRequestStatus Status { get; set; }

    public bool Accepted => Status == FriendRequestStatus.Accepted;
}
