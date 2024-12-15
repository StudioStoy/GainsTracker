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
    /// <summary>
    /// Gets all the workouts of a user. 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkoutDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetUserWorkouts() => 
        Ok(await service.GetWorkoutsByUsername(CurrentUsername));

    /// <summary>
    /// Registers a workout to the user's account.
    /// </summary>
    /// <param name="workout">The workout type to register.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AddWorkoutToAccount([FromBody] CreateWorkoutDto workout)
    {
        var createdWorkout = await service.AddWorkoutToGainsAccount(CurrentUsername, workout);
        return CreatedAtAction(nameof(AddWorkoutToAccount), new { id = createdWorkout.Id }, createdWorkout);
    }

    /// <summary>
    /// Gets the logged measurements of the specific workout.
    /// </summary>
    /// <param name="workoutId">The id of the workout.</param>
    /// <returns></returns>
    [HttpGet("{workoutId:guid}/measurement")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutMeasurementsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetWorkoutWithMeasurements(Guid workoutId) => 
        Ok(await service.GetWorkoutMeasurementsById(workoutId));

    /// <summary>
    /// Adds the given measurement to the workout.
    /// </summary>
    /// <param name="workoutId">The id of the workout.</param>
    /// <param name="measurementDto">The measurement data to add to the workout.</param>
    /// <returns></returns>
    [HttpPost("{workoutId:guid}/measurement")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AddMeasurementToWorkout(Guid workoutId,
        [FromBody] CreateMeasurementDto measurementDto)
    {
        var createdMeasurement = await service.AddMeasurementToWorkout(workoutId, measurementDto);
        return CreatedAtAction(nameof(AddMeasurementToWorkout), new { id = createdMeasurement.Id }, createdMeasurement);
    }
}
