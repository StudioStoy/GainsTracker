using System.ComponentModel.DataAnnotations;

namespace GainsTrackerAPI.Security.Controllers.DTO;

public class LoginRequestDto
{
    [Required] public string? Username { get; set; }
    [Required] public string? Password { get; set; }
}
