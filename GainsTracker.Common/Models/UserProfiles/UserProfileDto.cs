#region

using GainsTracker.Common.Models.Workouts.Dto;

#endregion

namespace GainsTracker.Common.Models.UserProfiles;

public class UserProfileDto
{
    public string? DisplayName { get; set; }
    public string? IconUrl { get; set; }
    public string? IconColor { get; set; }
    public string? Description { get; set; }
    public List<MeasurementDto>? PinnedPBs { get; set; }
}
