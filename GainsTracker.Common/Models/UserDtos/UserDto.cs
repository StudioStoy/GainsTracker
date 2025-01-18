namespace GainsTracker.Common.Models.UserDtos;

public record UserDto(
    Guid Id,
    string AuthId,
    Guid GainsAccountId,
    string UserHandle,
    string Email
);
