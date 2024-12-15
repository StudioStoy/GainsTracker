namespace GainsTracker.Common.Models.Users;

public record UserDto(
    string Id,
    string GainsAccountId,
    string UserHandle,
    string DisplayName
);
