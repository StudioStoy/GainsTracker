using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using GainsTracker.CoreAPI.Components.Workouts.Services;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace GainsTracker.CoreAPI.Components.Workouts.Controllers;

[ApiController]
[Authorize]
[Route("catalog/workout")]
public class WorkoutCatalogController : ExtendedControllerBase
{
    private readonly IWorkoutCatalogService _workoutCatalogService;

    public WorkoutCatalogController(IWorkoutCatalogService workoutCatalogService)
    {
        _workoutCatalogService = workoutCatalogService;
    }

    [HttpGet("types")]
    public IActionResult GetAvailableWorkoutTypes()
    {
        List<WorkoutTypeDto> workoutTypes = _workoutCatalogService.GetAllWorkoutTypes();
        return Ok(workoutTypes);
    }

    [HttpGet("available")]
    public IActionResult GetAvailableWorkoutsForUser()
    {
        return Ok(_workoutCatalogService.GetAvailableWorkoutTypesForUser(CurrentUsername));
    }
}
