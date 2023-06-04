using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Services;
using GainsTracker.CoreAPI.Components.Workouts.Services.Dto;
using GainsTracker.CoreAPI.Configurations.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Workouts.Controllers;

[ApiController]
[Authorize]
[Route("gains/workout")]
public class GainsControllerBase : ExtendedControllerBase
{
    private readonly IGainsService _gainsService;

    public GainsControllerBase(IGainsService service)
    {
        _gainsService = service;
    }

    [HttpGet("/gains/user")]
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
