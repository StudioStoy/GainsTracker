using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Interfaces.Services;

namespace GainsTracker.Core.Friends.Services;

public class FriendService(IFriendBigBrain bigBrain, IGainsService gainsService) : IFriendService
{
    public async Task<List<Friend>> GetFriends(string username)
    {
        Guid gainsId = await gainsService.GetGainsIdByUsername(username);
        return await bigBrain.GetFriendsByGainsId(gainsId);
    }
}
