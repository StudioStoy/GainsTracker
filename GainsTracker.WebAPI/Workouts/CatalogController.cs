using GainsTracker.Common.Models.Catalog;
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
    [HttpGet("workouts")]
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
    [HttpGet("measurements")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MeasurementExampleDto>))]
    public IActionResult GetExampleMeasurementRequests()
    {
        List<MeasurementExampleDto> examples =
        [
            new(
                ExerciseCategory.Reps.ToString(),
                new RepsMeasurementDto()
            ),
            new(
                ExerciseCategory.Strength.ToString(),
                new StrengthMeasurementDto()
            ),
            new(
                ExerciseCategory.TimeEndurance.ToString(),
                new TimeEnduranceMeasurementDto()
            ),
            new(
                ExerciseCategory.TimeDistanceEndurance.ToString(),
                new TimeDistanceEnduranceMeasurementDto()
            ),
            new(
                ExerciseCategory.General.ToString(),
                new GeneralMeasurementDto()
            ),
        ];

        return Ok(examples);
    }
}
