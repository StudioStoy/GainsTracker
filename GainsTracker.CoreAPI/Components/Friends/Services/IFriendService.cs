using GainsTracker.CoreAPI.Components.Friends.Models;

namespace GainsTracker.CoreAPI.Components.Friends.Services;

public interface IFriendService
{
    public List<Friend> GetFriends(string username);
}
