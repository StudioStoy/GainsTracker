using GainsTrackerAPI.Controllers.DTO;

namespace GainsTrackerAPI.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
