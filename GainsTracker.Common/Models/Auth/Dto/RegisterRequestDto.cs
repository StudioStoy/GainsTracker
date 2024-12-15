namespace GainsTracker.Common.Models.Auth.Dto;

public record RegisterRequestDto(
    string UserHandle,
    string Email,
    string Password,
    string? DisplayName
);
