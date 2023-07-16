using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Database;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Data;

public class BigBrainUserProfile : BigBrain
{
    public BigBrainUserProfile(AppDbContext context) : base(context)
    {
    }

    public UserProfile GetUserProfileByUserHandle(string userHandle)
    {
        string gainsId = GetGainsIdByUsername(userHandle);
        return Context.UserProfiles.FirstOrDefault(e => e.GainsAccountId == gainsId)
               ?? throw new NotFoundException($"User profile for gains account {gainsId} was not found.");
    }

    public void UpdateUserProfileByUserHandle(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        UserProfile current = GetUserProfileByUserHandle(userHandle);

        current.Description = userProfileDto.Description ?? current.Description;
        current.DisplayName = userProfileDto.DisplayName ?? current.DisplayName;
        current.Icon.Url = userProfileDto.IconUrl ?? current.Icon.Url;

        SaveContext();
    }
}
