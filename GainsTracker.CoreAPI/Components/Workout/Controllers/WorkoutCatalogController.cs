using GainsTracker.Common.DTO;
using GainsTracker.CoreAPI.Components.Workout.Models.Workouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace GainsTracker.CoreAPI.Components.Workout.Controllers;

[ApiController]
[Route("catalog")]
public class WorkoutCatalogController : ControllerBase
{
    [HttpGet("workout-types")]
    public IActionResult GetAvailableWorkoutTypes()
    {
        List<WorkoutTypeDto> workoutTypes = Enum.GetNames<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto(
                workoutType,
                WorkoutUtils.GetCategoryFromType(workoutType).GetDisplayName())
            ).ToList();

        return Ok(workoutTypes);
    }
}
