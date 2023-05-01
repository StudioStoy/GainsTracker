using System.Security.Claims;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Gains.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTrackerAPI.Gains.Controllers;

[ApiController]
[Authorize]
[Route("gains/user")]
public class GainsController : ControllerBase
{
    private readonly IGainsService _gainsService;

    public GainsController(IGainsService service)
    {
        _gainsService = service;
    }

    private string CurrentUserName => User.FindFirstValue(ClaimTypes.Name);

    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        List<Workout> workouts = await _gainsService.GetWorkoutsByUsername(CurrentUserName);
        return Ok(workouts);
    }

    [HttpGet("workout")]
    public async Task<IActionResult> GetUserWorkouts()
    {
        List<Workout> workouts = await _gainsService.GetWorkoutsByUsername(CurrentUserName);
        return Ok(workouts);
    }
}
