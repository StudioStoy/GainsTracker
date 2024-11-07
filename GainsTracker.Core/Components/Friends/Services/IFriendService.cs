using GainsTracker.Core.Components.Friends.Models;

namespace GainsTracker.Core.Components.Friends.Services;

public interface IFriendService
{
    public List<Friend> GetFriends(string username);
}
