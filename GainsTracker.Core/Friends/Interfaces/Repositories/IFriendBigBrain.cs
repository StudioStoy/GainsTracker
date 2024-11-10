using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Workouts.Models;

namespace GainsTracker.Core.Friends.Interfaces.Repositories;

public interface IFriendBigBrain : IBaseBrain
{
    Task<List<Friend>> GetFriendsByGainsId(Guid gainsId);
    Task<GainsAccount> GetFriendInfoByGainsId(Guid gainsId);
    Task<FriendRequest> GetFriendRequestById(Guid requestId);
}
