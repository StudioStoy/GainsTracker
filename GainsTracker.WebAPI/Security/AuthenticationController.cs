using GainsTracker.Common.Models.Auth.Dto;
using GainsTracker.Core.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Security;

[Route("auth")]
[ApiController]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    /// <summary>
    ///     Login with the given credentials.
    /// </summary>
    /// <param name="request">The DTO containing the user credentials</param>
    /// <returns>A JWT Bearer token to make subsequent requests with.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request) =>
        Ok(await authenticationService.Login(request));

    /// <summary>
    ///     Registers a new user with the given fields.
    /// </summary>
    /// <param name="request">The DTO containing the user data, optionally a display name.</param>
    /// <returns>A JWT Bearer token to make subsequent requests with.</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request) =>
        Ok(await authenticationService.Register(request));
}
