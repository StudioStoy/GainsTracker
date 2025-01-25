using GainsTracker.Common.Models.Generic;
using GainsTracker.Common.Models.Measurements.Enums.Units;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;
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
                GenericJsonSerializer.SerializeObjectToJson(new RepsMeasurementDto())
            ),
            new(
                ExerciseCategory.Strength.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new StrengthMeasurementDto())
            ),
            new(
                ExerciseCategory.TimeEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new TimeEnduranceMeasurementDto())
            ),
            new(
                ExerciseCategory.TimeAndDistanceEndurance.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(
                    new TimeDistanceEnduranceMeasurementDto())
            ),
            new(
                ExerciseCategory.General.ToString(),
                GenericJsonSerializer.SerializeObjectToJson(new GeneralMeasurementDto())
            ),
        ];

        return Ok(examples);
    }
}
