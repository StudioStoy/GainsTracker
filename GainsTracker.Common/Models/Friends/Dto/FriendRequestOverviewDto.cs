namespace GainsTracker.Common.Models.Friends.Dto;

public record FriendRequestOverviewDto(List<FriendRequestDto> Sent, List<FriendRequestDto> Received);
