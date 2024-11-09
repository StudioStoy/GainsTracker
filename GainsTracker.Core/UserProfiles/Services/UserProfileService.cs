using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Components.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.Components.UserProfiles.Interfaces.Services;

namespace GainsTracker.Core.Components.UserProfiles.Services;

public class UserProfileService(IUserProfileBigBrain bigBrain) : IUserProfileService
{
    public async Task UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        // Caveat: this profanity filter is not perfect. However, it is better than nothing
        ProfanityFilter.ProfanityFilter detector = new();
        if (detector.IsProfanity(userProfileDto.DisplayName) || detector.IsProfanity(userProfileDto.Description))
            throw new ArgumentException("no bad words buster");

        await bigBrain.UpdateUserProfileByUserHandle(userHandle, userProfileDto);
    }

    public async Task<UserProfileDto> GetUserProfile(string userHandle)
    {
        return (await bigBrain.GetUserProfileByUserHandle(userHandle)).ToDto();
    }

    public async Task<List<MeasurementDto>> GetPinnedPBs(string userHandle)
    {
        return (await bigBrain.GetPinnedPBs(userHandle))
            .Select(pb => pb.ToDto())
            .ToList();
    }

    public async Task UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        await bigBrain.AddAndRemovePBs(userHandle, pinnedPBsDto);
    }
}
