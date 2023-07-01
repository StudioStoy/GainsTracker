using GainsTracker.Common.Models.UserProfiles;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Services;

public interface IUserProfileService
{
    void UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto);
    UserProfileDto GetUserProfile(string userHandle);
    void UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto);
}
