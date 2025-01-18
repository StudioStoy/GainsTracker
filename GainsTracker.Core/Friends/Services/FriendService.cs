using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;

namespace GainsTracker.Core.Friends.Services;

public class FriendService(IFriendRepository repository) : IFriendService
{
    public async Task<List<FriendDto>> GetFriendsByGainsId(Guid gainsId)
    {
        var friends = await repository.GetFriendsByGainsId(gainsId);
        return friends.ToDtoList<Friend, FriendDto>();
    }
}
