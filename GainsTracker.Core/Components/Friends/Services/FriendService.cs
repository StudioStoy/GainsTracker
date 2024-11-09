using GainsTracker.Core.Components.Friends.Interfaces.Repositories;
using GainsTracker.Core.Components.Friends.Interfaces.Services;
using GainsTracker.Core.Components.Friends.Models;

namespace GainsTracker.Core.Components.Friends.Services;

public class FriendService(IFriendBigBrain bigBrain) : IFriendService
{
    public async Task<List<Friend>> GetFriends(string username)
    {
        Guid gainsId = await bigBrain.GetGainsIdByUsername(username);
        return await bigBrain.GetFriendsByGainsId(gainsId);
    }
}
