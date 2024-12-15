namespace GainsTracker.Common.Models.Friends.Dto;

public record FriendRequestDto(
    Guid Id,
    string RequesterName,
    string RecipientName,
    string RequestTime,
    string Status,
    Guid RequesterId,
    Guid RecipientId
);
