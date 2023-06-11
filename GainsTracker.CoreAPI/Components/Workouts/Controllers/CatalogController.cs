using System.Text.Json;
using GainsTracker.Common.Models.Generic;
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

        examples.Add("strengthMeasurement", GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto()));
        examples.Add("runningEnduranceMeasurement", GenericJsonSerializer.SerializeObjectToJson(new RunningEnduranceMeasurementDto()));
        examples.Add("simpleRepMeasurement", GenericJsonSerializer.SerializeObjectToJson(new SimpleRepMeasurementDto()));
        examples.Add("SimpleEnduranceMeasurement", GenericJsonSerializer.SerializeObjectToJson(new SimpleEnduranceMeasurementDto()));

        return Ok(examples);
    }
}
