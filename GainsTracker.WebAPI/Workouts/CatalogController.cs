using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Units;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Users.Interfaces;
using GainsTracker.Core.Workouts.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.WebAPI.Workouts;

[ApiController]
[Authorize]
[Route("catalog")]
public class CatalogController(ICatalogService catalogService, IUserService userService) : ExtendedControllerBase(
    userService)
{
    /// <summary>
    /// Gets a list of possible new workouts the user can start logging.
    /// </summary>
    /// <returns></returns>
    [HttpGet("workout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkoutTypeDto>))]
    public async Task<IActionResult> GetAvailableWorkoutsForUser()
    {
        var gainsId = (await GetCurrentUser()).GainsAccountId;
        return Ok(await catalogService.GetAvailableWorkoutTypesByGainsId(gainsId));
    }

    /// <summary>
    /// Gets a list of possible measurements the user can send for each exercise category. 
    /// </summary>
    /// <returns></returns>
    [HttpGet("measurement")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MeasurementTypeDto>))]
    public IActionResult GetExampleMeasurementRequests()
    {
        List<MeasurementTypeDto> examples =
        [
            new(
                ExerciseCategory.Reps.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new RepsMeasurementDto(0))
            ),
            new(
                ExerciseCategory.Strength.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto(WeightUnits.Kilograms, 0, 0))
            ),
            new(
                ExerciseCategory.TimeEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new TimeEnduranceMeasurementDto(0))
            ),
            new(
                ExerciseCategory.TimeAndDistanceEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(
                    new TimeAndDistanceEnduranceMeasurementDto(DistanceUnits.Kilometers, 0))
            ),
            new(
                ExerciseCategory.General.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new GeneralMeasurementDto(string.Empty))
            ),
        ];

        return Ok(examples);
    }
}
