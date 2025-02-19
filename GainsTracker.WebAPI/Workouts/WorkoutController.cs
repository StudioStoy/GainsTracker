using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Users.Interfaces;
using GainsTracker.Core.Workouts.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Workouts;

[ApiController]
[Authorize]
[Route("workouts")]
public class WorkoutController(IWorkoutService service, IUserService userService) : ExtendedControllerBase(userService)
{
    /// <summary>
    /// Gets all the workouts of a user. 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkoutDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetUserWorkouts()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        return Ok(await service.GetWorkoutsByGainsId(gainsId));
    }

    /// <summary>
    /// Adds a new workout it to the user's account and logs its first measurement.
    /// </summary>
    /// <param name="workout">The workout data to add.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> CreateWorkoutWithMeasurement([FromBody] AddNewWorkoutDto workout)
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        var createdWorkout = await service.AddWorkoutToGainsAccount(gainsId, workout);
        return CreatedAtAction(nameof(CreateWorkoutWithMeasurement), new { id = createdWorkout.Id }, createdWorkout);
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
    
    /// <summary>
    /// Gets all the personal best measurements of a user. 
    /// </summary>
    /// <returns></returns>
    [HttpGet("personal-bests")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkoutDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetPersonalBests()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        return Ok(await service.GetAllPersonalBestsByGainsId(gainsId));
    }
}
