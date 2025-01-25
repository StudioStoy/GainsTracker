using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.Common.Models.UserProfiles;

public record UserProfileDto(
    string? DisplayName,
    string? IconUrl,
    string? IconColor,
    string? Description,
    List<IMeasurementDto>? PinnedPBs
);
