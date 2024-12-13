using System.Text.Json;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Workouts.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Workouts;

[ApiController]
[Authorize]
[Route("catalog")]
public class CatalogController(ICatalogService catalogService) : ExtendedControllerBase
{
    [HttpGet("workout")]
    public async Task<IActionResult> GetAvailableWorkoutsForUser()
    {
        return Ok(await catalogService.GetAvailableWorkoutTypesForUser(CurrentUsername));
    }

    [HttpGet("measurement")]
    public IActionResult GetExampleMeasurementRequests()
    {
        Dictionary<string, JsonDocument> examples = new()
        {
            { ExerciseCategory.Reps.ToString(), GenericJsonSerializer.SerializeObjectToJson(new RepsMeasurementDto()) },
            {
                ExerciseCategory.Strength.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto())
            },
            {
                ExerciseCategory.TimeEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new TimeEnduranceMeasurementDto())
            },
            {
                ExerciseCategory.TimeAndDistanceEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new TimeAndDistanceEnduranceMeasurementDto())
            },
            {
                ExerciseCategory.General.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new GeneralMeasurementDto())
            },
        };

        return Ok(examples);
    }
}
