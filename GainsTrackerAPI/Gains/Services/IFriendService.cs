using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerAPI.Gains.Services.Dto;

namespace GainsTrackerAPI.Gains.Services;

public interface IFriendService
{
    public void SendFriendRequest(string username, string friendName);
    public void HandleFriendRequestState(string username, string requestId, bool accept = true);
    public List<Friend> GetFriends(string username);
    public FriendRequestOverviewDto GetFriendRequests(string username);
}
