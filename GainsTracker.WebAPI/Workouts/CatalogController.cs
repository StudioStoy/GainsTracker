using System.Text.Json;
using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Units;
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
    public async Task<IActionResult> GetAvailableWorkoutsForUser() =>
        Ok(await catalogService.GetAvailableWorkoutTypesForUser(CurrentUsername));

    [HttpGet("measurement")]
    public IActionResult GetExampleMeasurementRequests()
    {
        Dictionary<string, JsonDocument> examples = new()
        {
            {
                ExerciseCategory.Reps.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new RepsMeasurementDto(0))
            },
            {
                ExerciseCategory.Strength.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto(WeightUnits.Kilograms, 0, 0))
            },
            {
                ExerciseCategory.TimeEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new TimeEnduranceMeasurementDto(0))
            },
            {
                ExerciseCategory.TimeAndDistanceEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new TimeAndDistanceEnduranceMeasurementDto(DistanceUnits.Kilometers, 0))
            },
            {
                ExerciseCategory.General.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new GeneralMeasurementDto(string.Empty))
            },
        };

        return Ok(examples);
    }
}
