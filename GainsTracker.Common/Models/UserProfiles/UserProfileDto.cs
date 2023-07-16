using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Common.Models.UserProfiles;

public class UserProfileDto
{
    public string? DisplayName { get; set; } = string.Empty;
    public string? IconUrl { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public List<MeasurementDto>? PinnedPBs { get; set; } = new();
}
