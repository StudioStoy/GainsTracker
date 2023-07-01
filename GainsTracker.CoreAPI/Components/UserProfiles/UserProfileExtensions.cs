using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.UserProfiles;

public static class UserProfileExtensions
{
    public static UserProfileDto ToDto(this UserProfile userProfile)
    {
        return new UserProfileDto
        {
            Description = userProfile.Description,
            PictureUrl = userProfile.PictureUrl,
            PinnedPBs = userProfile.PinnedPBs.Select(pb => new MeasurementDto
            {
                WorkoutId = pb.WorkoutId,
                Category = pb.Category,
                TimeOfRecord = pb.TimeOfRecord,
                Notes = pb.Notes,
                Data = MeasurementFactory.SerializeMeasurementToJson(pb)
            }).ToList()
        };
    }
}
