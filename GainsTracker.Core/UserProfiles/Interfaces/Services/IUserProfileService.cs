using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.Core.UserProfiles.Interfaces.Services;

public interface IUserProfileService
{
    Task UpdateUserProfile(Guid gainsId, UpdateUserProfileDto userProfileDto);
    Task<UserProfileDto> GetUserProfile(Guid gainsId);
    Task UpdatePinnedPBs(Guid gainsId, UpdatePinnedPBsDto pinnedPBsDto);
    Task<List<IMeasurementDto>> GetPinnedPBs(Guid gainsId);
}
