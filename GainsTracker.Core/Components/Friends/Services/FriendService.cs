using GainsTracker.Core.Components.Friends.Data;
using GainsTracker.Data.Friends;
using Friend = GainsTracker.Core.Components.Friends.Models.Friend;

namespace GainsTracker.Core.Components.Friends.Services;

public class FriendService : IFriendService
{
    public FriendService(BigBrainFriend bigBrain)
    {
        _bigBrain = bigBrain;
    }

    private BigBrainFriend _bigBrain { get; }

    public List<Friend> GetFriends(string username)
    {
        string gainsId = _bigBrain.GetGainsIdByUsername(username);
        return _bigBrain.GetFriendsByGainsId(gainsId);
    }
}
