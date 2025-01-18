using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Models;

namespace GainsTracker.Core.Friends.Interfaces.Services;

public interface IFriendService
{
    public Task<List<FriendDto>> GetFriendsByGainsId(Guid userId);
}
