namespace GainsTracker.Common.Models.UserProfiles;

public class UpdateUserProfileDto
{
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public string? IconColorHex { get; set; }
}