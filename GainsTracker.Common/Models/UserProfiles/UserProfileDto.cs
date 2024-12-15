using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Common.Models.UserProfiles;

public record UserProfileDto(
    string? DisplayName,
    string? IconUrl,
    string? IconColor,
    string? Description,
    List<MeasurementDto>? PinnedPBs
);
