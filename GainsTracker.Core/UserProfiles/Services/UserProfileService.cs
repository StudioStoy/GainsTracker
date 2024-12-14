using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Interfaces.Services;

namespace GainsTracker.Core.UserProfiles.Services;

public class UserProfileService(IUserProfileRepository repository) : IUserProfileService
{
    public async Task UpdateUserProfile(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        // Caveat: this profanity filter is not perfect. However, it is better than nothing
        ProfanityFilter.ProfanityFilter detector = new();
        if (detector.IsProfanity(userProfileDto.DisplayName) || detector.IsProfanity(userProfileDto.Description))
            throw new ArgumentException("no bad words buster");

        await repository.UpdateUserProfileByUserHandle(userHandle, userProfileDto);
    }

    public async Task<UserProfileDto> GetUserProfile(string userHandle) =>
        (await repository.GetUserProfileByUserHandle(userHandle)).ToDto();

    public async Task<List<MeasurementDto>> GetPinnedPBs(string userHandle)
    {
        return (await repository.GetPinnedPBs(userHandle))
            .Select(pb => pb.ToDto())
            .ToList();
    }

    public async Task UpdatePinnedPBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        await repository.AddAndRemovePBs(userHandle, pinnedPBsDto);
    }
}
