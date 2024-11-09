namespace GainsTracker.Common.Models.Friends.Dto;

public class FriendRequestDto
{
    public FriendRequestDto(Guid id, string requesterName, string recipientName, string requestTime, string status, Guid requesterId, Guid recipientId)
    {
        Id = id;
        RequesterId = requesterId;
        RecipientId = recipientId;
        RequesterName = requesterName;
        RecipientName = recipientName;
        RequestTime = requestTime;
        Status = status;
    }

    public Guid Id { get; set; }

    public Guid RequesterId { get; set; }
    public Guid RecipientId { get; set; }

    public string RequesterName { get; set; }
    public string RecipientName { get; set; }

    public string RequestTime { get; set; }
    public string Status { get; set; }
}
