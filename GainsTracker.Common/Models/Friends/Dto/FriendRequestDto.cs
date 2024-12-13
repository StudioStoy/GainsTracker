namespace GainsTracker.Common.Models.Friends.Dto;

public class FriendRequestDto(
    Guid id,
    string requesterName,
    string recipientName,
    string requestTime,
    string status,
    Guid requesterId,
    Guid recipientId)
{
    public Guid Id { get; set; } = id;

    public Guid RequesterId { get; set; } = requesterId;
    public Guid RecipientId { get; set; } = recipientId;

    public string RequesterName { get; set; } = requesterName;
    public string RecipientName { get; set; } = recipientName;

    public string RequestTime { get; set; } = requestTime;
    public string Status { get; set; } = status;
}
