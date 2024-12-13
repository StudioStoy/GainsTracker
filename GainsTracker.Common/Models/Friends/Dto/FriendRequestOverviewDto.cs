namespace GainsTracker.Common.Models.Friends.Dto;

public class FriendRequestOverviewDto
{
    public List<FriendRequestDto> Sent { get; init; } = [];
    public List<FriendRequestDto> Received { get; set; } = [];
}
