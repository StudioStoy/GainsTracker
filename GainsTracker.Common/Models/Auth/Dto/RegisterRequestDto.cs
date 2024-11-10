using System.ComponentModel.DataAnnotations;

namespace GainsTracker.Common.Models.Auth.Dto;

public class RegisterRequestDto
{
    [Required] public string UserHandle { get; set; } = "";
    [Required] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
    public string? DisplayName { get; set; } = ""; // Optional
}
