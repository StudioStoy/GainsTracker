using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Components.Workouts.Interfaces;
using GainsTracker.Core.Components.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Components.Workouts.Interfaces.Services;
using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Services;

public class CatalogService(IWorkoutBigBrain bigBrain) : ICatalogService
{
    public async Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypesForUser(string username)
    {
        var gainsId = await bigBrain.GetGainsIdByUsername(username);
        var allWorkoutTypes = GetAllWorkoutTypes();
        var workouts = await bigBrain.GetWorkoutsByGainsId(gainsId);
        var activeWorkoutTypes = workouts
            .Select(w => new WorkoutTypeDto(w.Type.ToString(), ""));

        return allWorkoutTypes.Except(activeWorkoutTypes).ToList();
    }

    private List<WorkoutTypeDto> GetAllWorkoutTypes()
    {
        return Enum.GetNames<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto(
                workoutType,
                WorkoutUtils.GetCategoryFromType(workoutType).ToString())
            ).ToList();
    }
}
