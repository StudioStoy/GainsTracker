using DotnetBadWordDetector;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.CoreAPI.Components.UserProfiles.Data;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Services;

public class UserProfileService : IUserProfileService
{
    public UserProfileService(BigBrainUserProfile bigBrain)
    {
        _bigBrain = bigBrain;
    }

    private BigBrainUserProfile _bigBrain { get; }

    public void UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        // Caveat: this profanity filter is not perfect.
        // However, it is light-weight and does not depend on a static word list.
        ProfanityDetector detector = new();
        if (detector.IsProfane(userProfileDto.DisplayName) || detector.IsProfane(userProfileDto.Description))
            throw new ArgumentException("no bad words buster");

        _bigBrain.UpdateUserProfileByUserHandle(userHandle, userProfileDto);
    }

    public UserProfileDto GetUserProfile(string userHandle)
    {
        UserProfile userProfile = _bigBrain.GetUserProfileByUserHandle(userHandle);
        return userProfile.ToDto(_bigBrain.GetGainsAccountByUserHandle(userHandle).UserProfile.DisplayName);
    }

    public void UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        throw new NotImplementedException();
    }
}
