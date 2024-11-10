using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Workouts.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Workouts;

[ApiController]
[Authorize]
[Route("workouts")]
public class WorkoutController(IWorkoutService service) : ExtendedControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserWorkouts()
    {
        List<WorkoutDto> workouts = await service.GetWorkoutsByUsername(CurrentUsername);
        return Ok(workouts);
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkoutToAccount([FromBody] CreateWorkoutDto workout)
    {
        var result = await service.AddWorkoutToGainsAccount(CurrentUsername, workout);
        return Ok(result);
    }

    [HttpGet("{workoutId}/measurement")]
    public async Task<IActionResult> GetWorkoutWithMeasurements(Guid workoutId)
    {
        var measurements = await service.GetWorkoutMeasurementsById(workoutId);
        return Ok(measurements);
    }

    [HttpPost("{workoutId}/measurement")]
    public async Task<IActionResult> AddMeasurementToWorkout(Guid workoutId, [FromBody] CreateMeasurementDto measurementDto)
    {
        await service.AddMeasurementToWorkout(workoutId, measurementDto);
        return NoContent();
    }
}
