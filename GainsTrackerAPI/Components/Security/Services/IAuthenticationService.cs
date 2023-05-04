using GainsTrackerAPI.Components.Security.Controllers.DTO;

namespace GainsTrackerAPI.Components.Security.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
