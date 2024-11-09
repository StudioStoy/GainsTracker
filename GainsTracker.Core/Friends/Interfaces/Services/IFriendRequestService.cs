using GainsTracker.Common.Models.Friends.Dto;

namespace GainsTracker.Core.Components.Friends.Interfaces.Services;

public interface IFriendRequestService
{
    public Task SendFriendRequest(string username, string friendName);
    public Task HandleFriendRequestState(string username, Guid requestId, bool accept = true);
    public Task<FriendRequestOverviewDto> GetFriendRequests(string username);
}
