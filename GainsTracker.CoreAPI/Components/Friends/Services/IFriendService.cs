using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.CoreAPI.Components.Friends.Models;

namespace GainsTracker.CoreAPI.Components.Friends.Services;

public interface IFriendService
{
    public void SendFriendRequest(string username, string friendName);
    public void HandleFriendRequestState(string username, string requestId, bool accept = true);
    public List<Friend> GetFriends(string username);
    public FriendRequestOverviewDto GetFriendRequests(string username);
}
