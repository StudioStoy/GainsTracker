using System.ComponentModel.DataAnnotations.Schema;
using GainsTracker.Common.Models.Friends;
using GainsTracker.Data.Gains.Entities;

namespace GainsTracker.Data.Friends.Entities;

[Table("friend_request")]
public class FriendRequestEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RequesterId { get; set; }
    public required GainsAccountEntity Requester { get; set; }
    public Guid RecipientId { get; set; }
    public required GainsAccountEntity Recipient { get; set; }

    public DateTime RequestTime { get; set; }
    public FriendRequestStatus Status { get; set; }
}
