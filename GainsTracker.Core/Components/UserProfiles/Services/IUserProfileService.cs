using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Core.Components.UserProfiles.Services;

public interface IUserProfileService
{
    void UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto);
    UserProfileDto GetUserProfile(string userHandle);
    void UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto);
    List<MeasurementDto> GetPinnedPBs(string userHandle);
}
