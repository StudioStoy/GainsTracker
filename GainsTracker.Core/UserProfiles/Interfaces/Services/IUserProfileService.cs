using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Core.UserProfiles.Interfaces.Services;

public interface IUserProfileService
{
    Task UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto);
    Task<UserProfileDto> GetUserProfile(string userHandle);
    Task UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto);
    Task<List<MeasurementDto>> GetPinnedPBs(string userHandle);
}

