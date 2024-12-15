namespace GainsTracker.Common.Models.UserProfiles;

public record UpdateUserProfileDto(
    string? DisplayName,
    string? Description,
    string? IconUrl,
    string? IconColorHex
);
