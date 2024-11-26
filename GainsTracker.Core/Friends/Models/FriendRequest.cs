using GainsTracker.Common.Models.Friends;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Friends.Models;

public class FriendRequest(GainsAccount requester, GainsAccount recipient)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RequesterId { get; init; } = requester.Id;
    public Guid RecipientId { get; init; } = recipient.Id;

    public GainsAccount Requester { get; } = requester;
    public GainsAccount Recipient { get; } = recipient;

    public DateTime RequestTime { get; init; } = DateTime.UtcNow;
    public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

    public bool IsAccepted => Status == FriendRequestStatus.Accepted;

    public void Accept()
    {
        Status = FriendRequestStatus.Accepted;

        //TODO: Maybe emit an event or something for notifications?
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
