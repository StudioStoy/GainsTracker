using System.ComponentModel.DataAnnotations;

namespace GainsTracker.Core.Components.Security.DTO;

public class RegisterRequestDto
{
    [Required] public string UserHandle { get; set; } = "";
    [Required] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
    public string? DisplayName { get; set; } = ""; // Optional
}
