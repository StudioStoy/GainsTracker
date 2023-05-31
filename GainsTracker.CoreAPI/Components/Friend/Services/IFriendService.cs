using GainsTracker.CoreAPI.Components.Friend.Services.Dto;

namespace GainsTracker.CoreAPI.Components.Friend.Services;

public interface IFriendService
{
    public void SendFriendRequest(string username, string friendName);
    public void HandleFriendRequestState(string username, string requestId, bool accept = true);
    public List<Models.Friend> GetFriends(string username);
    public FriendRequestOverviewDto GetFriendRequests(string username);
}
