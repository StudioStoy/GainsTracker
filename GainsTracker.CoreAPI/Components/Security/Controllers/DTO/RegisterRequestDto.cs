using System.ComponentModel.DataAnnotations;

namespace GainsTracker.CoreAPI.Components.Security.Controllers.DTO;

public class RegisterRequestDto
{
    [Required] public string UserHandle { get; set; } = "";
    [Required] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
}
