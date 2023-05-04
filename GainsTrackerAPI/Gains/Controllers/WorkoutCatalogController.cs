using GainsTrackerAPI.Gains.Models;
using GainsTrackerLib.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace GainsTrackerAPI.Gains.Controllers;

[ApiController]
[Route("catalog")]
public class WorkoutCatalogController : ControllerBase
{
    [HttpGet("workout-types")]
    public IActionResult GetAvailableWorkoutTypes()
    {
        List<WorkoutTypeDto> workoutTypes = Enum.GetNames<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto
            {
                Type = workoutType,
                Category = WorkoutUtils.GetCategoryFromType(workoutType).GetDisplayName()
            }).ToList();

        return Ok(workoutTypes);
    }
}
