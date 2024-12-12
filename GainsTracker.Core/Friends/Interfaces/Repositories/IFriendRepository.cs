using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Friends.Interfaces.Repositories;

public interface IFriendRepository : IGenericRepository<Friend>
{
    Task<List<Friend>> GetFriendsByGainsId(Guid gainsId);
    Task<GainsAccount> GetFriendInfoByGainsId(Guid gainsId);
    Task<FriendRequest> GetFriendRequestById(Guid requestId);
    Task UpdateFriendRequest(FriendRequest request);
    Task AddFriendRequest(FriendRequest request);
}
