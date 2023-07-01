using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.CoreAPI.Components.UserProfiles.Data;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Services;

public class UserProfileService : IUserProfileService
{
    private BigBrainUserProfile _bigBrain { get; }

    public UserProfileService(BigBrainUserProfile bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public void UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        //TODO: apply filters here for bad words n shizzle
        _bigBrain.UpdateUserProfileByUserHandle(userHandle, userProfileDto);
    }

    public UserProfileDto GetUserProfile(string userHandle)
    {
        UserProfile userProfile = _bigBrain.GetUserProfileByUserHandle(userHandle);
        return userProfile.ToDto();
    }

    public void UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        throw new NotImplementedException();
    }
}
