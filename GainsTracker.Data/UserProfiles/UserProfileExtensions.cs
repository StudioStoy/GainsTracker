using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Data.UserProfiles.Entities;
using GainsTracker.Data.Workouts;

namespace GainsTracker.Data.UserProfiles;

public static class UserProfileExtensions
{
    // UserProfile
    public static UserProfile ToModel(this UserProfileEntity entity)
    {
        return new UserProfile(entity.GainsAccountId, entity.DisplayName)
        {
            Id = entity.Id,
            Description = entity.Description,
            PinnedPBs = entity.PinnedPBs.Select(measurement => measurement.ToModel()).ToList(),
            Icon = entity.Icon.ToModel(),
        };
    }

    public static UserProfileEntity ToEntity(this UserProfile model)
    {
        return new UserProfileEntity
        {
            Id = model.Id,
            GainsAccountId = model.GainsAccountId,
            DisplayName = model.DisplayName,
            Description = model.Description,
            PinnedPBs = model.PinnedPBs.Select(measurement => measurement.ToEntity()).ToList(),
            Icon = model.Icon.ToEntity(),
        };
    }

    // ProfileIcon
    public static ProfileIcon ToModel(this ProfileIconEntity entity)
    {
        return new ProfileIcon
        {
            Id = entity.Id,
            UserProfileId = entity.UserProfileId,
            PictureColor = entity.PictureColor,
            Url = entity.Url,
        };
    }

    public static ProfileIconEntity ToEntity(this ProfileIcon model)
    {
        return new ProfileIconEntity
        {
            Id = model.Id,
            UserProfileId = model.UserProfileId,
            PictureColor = model.PictureColor,
            Url = model.Url,
        };
    }
}
