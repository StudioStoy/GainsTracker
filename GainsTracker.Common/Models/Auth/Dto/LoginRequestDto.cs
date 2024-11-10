using System.ComponentModel.DataAnnotations;

namespace GainsTracker.Common.Models.Auth.Dto;

public class LoginRequestDto
{
    [Required] public string UserHandle { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}
