using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Friends.Services.Dto;

namespace GainsTracker.CoreAPI.Components.Friends.Services;

public interface IFriendService
{
    public void SendFriendRequest(string username, string friendName);
    public void HandleFriendRequestState(string username, string requestId, bool accept = true);
    public List<Friend> GetFriends(string username);
    public FriendRequestOverviewDto GetFriendRequests(string username);
}
