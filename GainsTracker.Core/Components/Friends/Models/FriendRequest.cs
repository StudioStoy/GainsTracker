using GainsTracker.Common.Models.Friends;
using GainsTracker.Core.Components.Workouts.Models;

namespace GainsTracker.Core.Components.Friends.Models;

public class FriendRequest
{
    public FriendRequest(GainsAccount requester, GainsAccount recipient)
    {
        Requester = requester;
        Recipient = recipient;
        RequesterId = requester.Id;
        RecipientId = recipient.Id;

        Status = FriendRequestStatus.Pending;
        RequestTime = DateTime.UtcNow;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RequesterId { get; set; }
    public Guid RecipientId { get; set; }

    public GainsAccount Requester { get; set; }
    public GainsAccount Recipient { get; set; }

    public DateTime RequestTime { get; set; }
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
