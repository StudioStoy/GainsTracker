using GainsTracker.Core.Components.Security.Controllers.DTO;
using GainsTracker.Core.Components.Security.Services;

namespace GainsTracker.Core.Components.Security.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    ///     Login with the given credentials. If valid, returns a JWT.
    /// </summary>
    /// <param name="request">The DTO containing the user credentials</param>
    /// <returns>A JWT Bearer token to make subsequent requests with.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        return Ok(await _authenticationService.Login(request));
    }

    /// <summary>
    ///     Registers a new user with the given fields.
    /// </summary>
    /// <param name="request">The DTO containing the user data, optionally a display name.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        string response = await _authenticationService.Register(request);

        return Ok(response);
    }
}
