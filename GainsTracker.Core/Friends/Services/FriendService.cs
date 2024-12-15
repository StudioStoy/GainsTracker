using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Interfaces.Services;

namespace GainsTracker.Core.Friends.Services;

public class FriendService(IFriendRepository repository, IGainsService gainsService) : IFriendService
{
    public async Task<List<FriendDto>> GetFriends(string username)
    {
        var gainsId = await gainsService.GetGainsIdByUsername(username);
        var friends = await repository.GetFriendsByGainsId(gainsId);
        return friends.ToDtoList<Friend, FriendDto>();
    }
}
