using GainsTracker.Common.Models.Friends;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Friends.Models;

public class FriendRequest
{
    public FriendRequest()
    {
    }

    public FriendRequest(GainsAccount requester, GainsAccount recipient)
    {
        Requester = requester;
        RequesterId = requester.Id;
        Recipient = recipient;
        RecipientId = recipient.Id;
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid RequesterId { get; init; }
    public GainsAccount Requester { get; set; } = null!;
    public Guid RecipientId { get; init; }
    public GainsAccount Recipient { get; set; } = null!;

    public DateTime RequestTime { get; init; } = DateTime.UtcNow;
    public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

    public bool IsAccepted => Status == FriendRequestStatus.Accepted;

    public void Accept()
    {
        Status = FriendRequestStatus.Accepted;

        Requester.SentFriendRequests.Remove(this);
        Recipient.ReceivedFriendRequests.Remove(this);

        Requester.Friends.Add(new Friend(Recipient, DateTime.UtcNow));
        Recipient.Friends.Add(new Friend(Requester, DateTime.UtcNow));
    }

    public void Reject()
    {
        Status = FriendRequestStatus.Rejected;

        Requester.SentFriendRequests.Remove(this);
        Recipient.ReceivedFriendRequests.Remove(this);
    }
}
