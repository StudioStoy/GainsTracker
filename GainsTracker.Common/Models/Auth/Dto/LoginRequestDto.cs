using System.ComponentModel.DataAnnotations;

namespace GainsTracker.Core.Components.Security.DTO;

public class LoginRequestDto
{
    [Required] public string UserHandle { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}
