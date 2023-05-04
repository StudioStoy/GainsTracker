using System.ComponentModel.DataAnnotations;

namespace GainsTrackerAPI.Components.Security.Controllers.DTO;

public class RegisterRequestDto
{
    [Required] public string? Username { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public string? Password { get; set; }
}
