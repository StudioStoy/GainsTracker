using System.Security.Claims;
using GainsTracker.Common;
using GainsTracker.CoreAPI.Components.Gains.Models;
using GainsTracker.CoreAPI.Components.Gains.Services;
using GainsTracker.CoreAPI.Components.Gains.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Gains.Controllers;

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

    private string CurrentUsername => User.FindFirstValue(ClaimTypes.Name) ?? Constants.AnonymousUserName;

    [HttpGet]
    public IActionResult GetUserGains()
    {
        GainsAccount account = _gainsService.GetGainsAccountFromUser(CurrentUsername);
        return Ok(account);
    }

    [HttpGet("workout")]
    public async Task<IActionResult> GetUserWorkouts()
    {
        List<WorkoutDto> workouts = await _gainsService.GetWorkoutsByUsername(CurrentUsername);
        return Ok(workouts);
    }

    [HttpPost("workout")]
    public IActionResult AddWorkoutToAccount([FromBody] WorkoutDto workout)
    {
        _gainsService.AddWorkoutToGainsAccount(CurrentUsername, workout);
        return NoContent();
    }

    [HttpGet("workout/{workoutId}/measurement")]
    public IActionResult GetWorkoutWithMeasurements(string workoutId)
    {
        return Ok(_gainsService.GetWorkoutMeasurementsById(workoutId));
    }

    [HttpPost("workout/{workoutId}/measurement")]
    public IActionResult AddMeasurementToWorkout(string workoutId, [FromBody] MeasurementDto measurementDto)
    {
        _gainsService.AddMeasurementToWorkout(workoutId, measurementDto);
        return NoContent();
    }
}
