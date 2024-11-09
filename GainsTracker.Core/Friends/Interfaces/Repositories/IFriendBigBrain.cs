using GainsTracker.Core.Components.Friends.Models;
using GainsTracker.Core.Components.Workouts.Models;

namespace GainsTracker.Core.Components.Friends.Interfaces.Repositories;

public interface IFriendBigBrain : IBigBrain<Friend>
{
    Task<List<Friend>> GetFriendsByGainsId(Guid gainsId);
    Task<GainsAccount> GetFriendInfoByGainsId(Guid gainsId);
    Task<FriendRequest> GetFriendRequestById(Guid requestId);
}
