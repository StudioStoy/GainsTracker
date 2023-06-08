using GainsTracker.Common.Models.Friends.Dto;

namespace GainsTracker.CoreAPI.Components.Friends.Services;

public interface IFriendRequestService
{
    public void SendFriendRequest(string username, string friendName);
    public void HandleFriendRequestState(string username, string requestId, bool accept = true);
    public FriendRequestOverviewDto GetFriendRequests(string username);
}
