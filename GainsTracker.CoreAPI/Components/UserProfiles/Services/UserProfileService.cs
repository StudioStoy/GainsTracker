using DotnetBadWordDetector;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.UserProfiles.Data;
using GainsTracker.CoreAPI.Shared;

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
        return _bigBrain
            .GetUserProfileByUserHandle(userHandle, () => u => u.Icon)
            .ToDto();
    }

    public List<MeasurementDto> GetPinnedPBs(string userHandle)
    {
        return _bigBrain.GetPinnedPBs(userHandle);
    }

    public void UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        _bigBrain.AddAndRemovePBs(userHandle, pinnedPBsDto);
    }
}
