#region

using GainsTracker.Common.Models.Auth.Dto;

#endregion

namespace GainsTracker.Core.Auth.Interfaces;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
