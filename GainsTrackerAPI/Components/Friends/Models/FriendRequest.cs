using GainsTrackerAPI.Components.Gains.Models;

namespace GainsTrackerAPI.Components.Friends.Models;

public class FriendRequest
{
    // EF Core constructor.
    private FriendRequest()
    {
    }

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

    public GainsAccount RequestedBy { get; }
    public GainsAccount RequestedTo { get; }

    public DateTime RequestTime { get; private set; }
    public FriendRequestStatus Status { get; set; }

    public bool Accepted => Status == FriendRequestStatus.Accepted;

    public void Accept()
    {
        Status = FriendRequestStatus.Accepted;
        //TODO: Maybe sent an event or something for notifications?
        RequestedBy.SentFriendRequests.Remove(this);
        RequestedTo.ReceivedFriendRequests.Remove(this);

        RequestedBy.Friends.Add(new Friend(RequestedTo, DateTime.UtcNow));
        RequestedTo.Friends.Add(new Friend(RequestedBy, DateTime.UtcNow));
    }

    public void Reject()
    {
        Status = FriendRequestStatus.Rejected;
        // No event, happens silently.
        RequestedBy.SentFriendRequests.Remove(this);
        RequestedTo.ReceivedFriendRequests.Remove(this);
    }
}
