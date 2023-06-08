namespace GainsTracker.Common.Models.Friends.Dto;

public class FriendRequestDto
{
    public FriendRequestDto(string id, string requesterName, string recipientName, string requestTime, string status, string requesterId, string recipientId)
    {
        Id = id;
        RequesterId = requesterId;
        RecipientId = recipientId;
        RequesterName = requesterName;
        RecipientName = recipientName;
        RequestTime = requestTime;
        Status = status;
    }

    public string Id { get; set; }

    public string RequesterId { get; set; }
    public string RecipientId { get; set; }

    public string RequesterName { get; set; }
    public string RecipientName { get; set; }

    public string RequestTime { get; set; }
    public string Status { get; set; }
}
