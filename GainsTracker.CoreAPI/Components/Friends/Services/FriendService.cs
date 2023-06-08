using GainsTracker.CoreAPI.Components.Friends.Data;
using GainsTracker.CoreAPI.Components.Friends.Models;

namespace GainsTracker.CoreAPI.Components.Friends.Services;

public class FriendService : IFriendService
{
    public FriendService(BigBrainFriend bigBrain)
    {
        _bigBrain = bigBrain;
    }

    private BigBrainFriend _bigBrain { get; set; }
    
    public List<Friend> GetFriends(string username)
    {
        string gainsId = _bigBrain.GetGainsIdByUsername(username);
        return _bigBrain.GetFriendsByGainsId(gainsId);
    }
}
