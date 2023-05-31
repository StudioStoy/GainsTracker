using System.Security.Claims;
using GainsTracker.Common;
using GainsTracker.CoreAPI.Components.Workout.Models;
using GainsTracker.CoreAPI.Components.Workout.Services;
using GainsTracker.CoreAPI.Components.Workout.Services.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Workout.Controllers;

[ApiController]
[Authorize]
[Route("gains/workout")]
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

    [HttpGet]
    public async Task<IActionResult> GetUserWorkouts()
    {
        List<WorkoutDto> workouts = await _gainsService.GetWorkoutsByUsername(CurrentUsername);
        return Ok(workouts);
    }

    [HttpPost]
    public IActionResult AddWorkoutToAccount([FromBody] WorkoutDto workout)
    {
        _gainsService.AddWorkoutToGainsAccount(CurrentUsername, workout);
        return NoContent();
    }

    [HttpGet("{workoutId}/measurement")]
    public IActionResult GetWorkoutWithMeasurements(string workoutId)
    {
        return Ok(_gainsService.GetWorkoutMeasurementsById(workoutId));
    }

    [HttpPost("{workoutId}/measurement")]
    public IActionResult AddMeasurementToWorkout(string workoutId, [FromBody] MeasurementDto measurementDto)
    {
        _gainsService.AddMeasurementToWorkout(workoutId, measurementDto);
        return NoContent();
    }
}
