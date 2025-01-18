using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Interfaces.Services;

namespace GainsTracker.Core.UserProfiles.Services;

public class UserProfileService(IUserProfileRepository repository) : IUserProfileService
{
    public async Task UpdateUserProfile(Guid gainsId, UpdateUserProfileDto userProfileDto)
    {
        // Caveat: this profanity filter is not perfect. However, it is better than nothing.
        ProfanityFilter.ProfanityFilter detector = new();
        if (detector.IsProfanity(userProfileDto.DisplayName) || detector.IsProfanity(userProfileDto.Description))
            throw new ArgumentException("no bad words buster");

        await repository.UpdateUserProfileByUserHandle(gainsId, userProfileDto);
    }

    public async Task<UserProfileDto> GetUserProfile(Guid gainsId) =>
        (await repository.GetUserProfileByGainsId(gainsId)).ToDto();

    public async Task<List<MeasurementDto>> GetPinnedPBs(Guid gainsId)
    {
        return (await repository.GetPinnedPBs(gainsId))
            .Select(pb => pb.ToDto())
            .ToList();
    }

    public async Task UpdatePinnedPBs(Guid gainsId, UpdatePinnedPBsDto pinnedPBsDto)
    {
        await repository.AddAndRemovePBs(gainsId, pinnedPBsDto);
    }
}
