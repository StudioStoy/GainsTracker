using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerAPI.Gains.Services.Dto;

namespace GainsTrackerAPI.Gains.Services;

public interface IFriendService
{
    public Task SendFriendRequest(string username, string friendName);
    public Task<List<Friend>> GetFriends(string username);
    public Task<FriendRequestOverviewDto> GetFriendRequests(string username);
}
