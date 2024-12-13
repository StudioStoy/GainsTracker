#region

using GainsTracker.Core.Friends.Models;

#endregion

namespace GainsTracker.Core.Friends.Interfaces.Services;

public interface IFriendService
{
    public Task<List<Friend>> GetFriends(string username);
}
