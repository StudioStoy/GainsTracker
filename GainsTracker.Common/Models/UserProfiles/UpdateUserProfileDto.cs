namespace GainsTracker.Common.Models.UserProfiles;

public class UpdateUserProfileDto
{
    public string? PictureUrl { get; set; } = string.Empty;
    public string? DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
