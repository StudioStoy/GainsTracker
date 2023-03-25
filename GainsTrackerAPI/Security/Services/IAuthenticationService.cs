using GainsTrackerAPI.Security.Controllers.DTO;

namespace GainsTrackerAPI.Security.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
