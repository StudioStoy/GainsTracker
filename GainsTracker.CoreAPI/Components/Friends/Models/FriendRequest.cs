using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Components.Friends.Models;

public class FriendRequest
{
    // EF Core constructor.
    private FriendRequest()
    {
    }

    public FriendRequest(GainsAccount requester, GainsAccount recipient)
    {
        Requester = requester;
        Recipient = recipient;
        RequesterId = requester.Id;
        RecipientId = recipient.Id;

        Status = FriendRequestStatus.Pending;
        RequestTime = DateTime.UtcNow;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string RequesterId { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;

    public GainsAccount Requester { get; set; } = new();
    public GainsAccount Recipient { get; set; } = new();

    public DateTime RequestTime { get; private set; }
    public FriendRequestStatus Status { get; set; }

    public bool Accepted => Status == FriendRequestStatus.Accepted;

    public void Accept()
    {
        Status = FriendRequestStatus.Accepted;

        //TODO: Maybe sent an event or something for notifications?
        Requester.SentFriendRequests.Remove(this);
        Recipient.ReceivedFriendRequests.Remove(this);

        Requester.Friends.Add(new Friend(Recipient, DateTime.UtcNow));
        Recipient.Friends.Add(new Friend(Requester, DateTime.UtcNow));
    }

    public void Reject()
    {
        Status = FriendRequestStatus.Rejected;

        // No event, happens silently.
        Requester.SentFriendRequests.Remove(this);
        Recipient.ReceivedFriendRequests.Remove(this);
    }
}
