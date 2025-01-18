using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Core.UserProfiles.Interfaces.Services;

public interface IUserProfileService
{
    Task UpdateUserProfile(Guid gainsId, UpdateUserProfileDto userProfileDto);
    Task<UserProfileDto> GetUserProfile(Guid gainsId);
    Task UpdatePinnedPBs(Guid gainsId, UpdatePinnedPBsDto pinnedPBsDto);
    Task<List<MeasurementDto>> GetPinnedPBs(Guid gainsId);
}
