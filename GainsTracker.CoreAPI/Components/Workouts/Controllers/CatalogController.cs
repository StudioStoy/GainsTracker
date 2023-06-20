using System.Text.Json;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Services;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Components.Workouts.Controllers;

[ApiController]
[Authorize]
[Route("catalog")]
public class CatalogController : ExtendedControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("workout")]
    public IActionResult GetAvailableWorkoutsForUser()
    {
        return Ok(_catalogService.GetAvailableWorkoutTypesForUser(CurrentUsername));
    }

    [HttpGet("measurement")]
    public IActionResult GetExampleMeasurementRequests()
    {
        Dictionary<string, JsonDocument> examples = new();

        examples.Add(ExerciseCategory.Reps.ToString(), GenericJsonSerializer.SerializeObjectToJson(new RepsMeasurementDto()));
        examples.Add(ExerciseCategory.Strength.ToString(), GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto()));
        examples.Add(ExerciseCategory.TimeEndurance.ToString(), GenericJsonSerializer.SerializeObjectToJson(new TimeEnduranceMeasurementDto()));
        examples.Add(ExerciseCategory.TimeAndDistanceEndurance.ToString(), GenericJsonSerializer.SerializeObjectToJson(new TimeAndDistanceEnduranceMeasurementDto()));
        examples.Add(ExerciseCategory.General.ToString(), GenericJsonSerializer.SerializeObjectToJson(new GeneralMeasurementDto()));

        return Ok(examples);
    }
}
