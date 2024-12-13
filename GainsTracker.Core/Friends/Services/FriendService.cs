#region

using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Interfaces.Services;

#endregion

namespace GainsTracker.Core.Friends.Services;

public class FriendService(IFriendRepository repository, IGainsService gainsService) : IFriendService
{
    public async Task<List<Friend>> GetFriends(string username)
    {
        var gainsId = await gainsService.GetGainsIdByUsername(username);
        return await repository.GetFriendsByGainsId(gainsId);
    }
}
