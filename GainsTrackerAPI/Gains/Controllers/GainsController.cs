using System.Security.Claims;
using GainsTrackerAPI.Gains.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTrackerAPI.Gains.Controllers;

[ApiController]
[Authorize]
[Route("gains")]
public class GainsController : ControllerBase
{
    private readonly IGainsService _gainsService;

    public GainsController(IGainsService service)
    {
        _gainsService = service;
    }

    [HttpGet("user/workout")]
    public async Task<IActionResult> GetUserWorkouts()
    {
        string? username = User.FindFirstValue(ClaimTypes.Name);
        return Ok(await _gainsService.GetWorkoutsByUsername(username));
    }
}
