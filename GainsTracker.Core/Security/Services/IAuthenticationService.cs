using GainsTracker.Core.Components.Security.DTO;

namespace GainsTracker.Core.Components.Security.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
