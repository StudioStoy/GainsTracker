using GainsTracker.Core.Components.Friends.Models;

namespace GainsTracker.Core.Components.Friends.Interfaces.Services;

public interface IFriendService
{
    public Task<List<Friend>> GetFriends(string username);
}
