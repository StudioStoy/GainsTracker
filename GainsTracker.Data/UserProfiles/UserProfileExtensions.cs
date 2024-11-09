using GainsTracker.Core.Components.UserProfiles.Models;
using GainsTracker.Data.Workouts;

namespace GainsTracker.Data.UserProfiles;

public static class UserProfileExtensions
{
    // UserProfile
    public static UserProfile MapToModel(this UserProfileEntity entity)
    {
        return new UserProfile(entity.GainsAccountId, entity.DisplayName)
        {
            Id = entity.Id,
            Description = entity.Description,
            PinnedPBs = entity.PinnedPBs.Select(measurement => measurement.MapToModel()).ToList(),
            Icon = entity.Icon.MapToModel()
        };
    }

    public static UserProfileEntity MapToEntity(this UserProfile model)
    {
        return new UserProfileEntity
        {
            Id = model.Id,
            GainsAccountId = model.GainsAccountId,
            DisplayName = model.DisplayName,
            Description = model.Description,
            PinnedPBs = model.PinnedPBs.Select(measurement => measurement.MapToEntity()).ToList(),
            Icon = model.Icon.MapToEntity()
        };
    }

    // ProfileIcon
    public static ProfileIcon MapToModel(this ProfileIconEntity entity)
    {
        return new ProfileIcon
        {
            Id = entity.Id,
            UserProfileId = entity.UserProfileId,
            PictureColor = entity.PictureColor,
            Url = entity.Url
        };
    }

    public static ProfileIconEntity MapToEntity(this ProfileIcon model)
    {
        return new ProfileIconEntity
        {
            Id = model.Id,
            UserProfileId = model.UserProfileId,
            PictureColor = model.PictureColor,
            Url = model.Url
        };
    }
}
