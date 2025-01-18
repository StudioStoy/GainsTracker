using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.UserProfiles.Interfaces.Repositories;

public interface IUserProfileRepository : IGenericRepository<UserProfile>
{
    Task UpdateUserProfileByUserHandle(Guid gainsId, UpdateUserProfileDto userProfileDto);
    Task<List<Measurement>> GetPinnedPBs(Guid gainsId);
    Task AddAndRemovePBs(Guid gainsId, UpdatePinnedPBsDto pinnedPBsDto);
    Task<UserProfile> GetUserProfileByGainsId(Guid gainsId);
}
