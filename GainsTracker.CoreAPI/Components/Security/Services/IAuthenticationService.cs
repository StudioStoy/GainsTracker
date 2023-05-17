using GainsTracker.CoreAPI.Components.Security.Controllers.DTO;

namespace GainsTracker.CoreAPI.Components.Security.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
