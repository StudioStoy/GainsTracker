namespace GainsTracker.Common.Models.UserProfiles;

public class UpdateUserProfileDto
{
    public string? DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? IconUrl { get; set; } = string.Empty;
    public string? IconColorHex { get; set; } = string.Empty;
}
