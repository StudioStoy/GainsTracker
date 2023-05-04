using System.ComponentModel.DataAnnotations;

namespace GainsTrackerAPI.Components.Security.Controllers.DTO;

public class LoginRequestDto
{
    [Required] public string? Username { get; set; }
    [Required] public string? Password { get; set; }
}
