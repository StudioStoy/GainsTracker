using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Services;

public class CatalogService(IWorkoutBigBrain bigBrain, IGainsService gainsService) : ICatalogService
{
    public async Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypesForUser(string username)
    {
        var gainsId = await gainsService.GetGainsIdByUsername(username);
        var allWorkoutTypes = GetAllWorkoutTypes();
        var workouts = await bigBrain.GetWorkoutsByGainsId(gainsId);
        var activeWorkoutTypes = workouts
            .Select(w => new WorkoutTypeDto(w.Type.ToString(), ""));

        return allWorkoutTypes.Except(activeWorkoutTypes).ToList();
    }

    private static List<WorkoutTypeDto> GetAllWorkoutTypes()
    {
        return Enum.GetNames<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto(
                workoutType,
                WorkoutUtils.GetCategoryFromType(workoutType).ToString())
            ).ToList();
    }
}
