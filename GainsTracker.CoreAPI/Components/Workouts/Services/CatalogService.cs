using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Data;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using Microsoft.OpenApi.Extensions;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public class CatalogService : ICatalogService
{
    private readonly BigBrainWorkout _bigBrain;

    public CatalogService(BigBrainWorkout bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public List<WorkoutTypeDto> GetAvailableWorkoutTypesForUser(string username)
    {
        string gainsId = _bigBrain.GetGainsIdByUsername(username);
        List<WorkoutTypeDto> allWorkoutTypes = GetAllWorkoutTypes();
        IEnumerable<WorkoutTypeDto> activeWorkoutTypes = _bigBrain.GetWorkoutsByGainsId(gainsId).Select(w => new WorkoutTypeDto(w.WorkoutType.ToString(), ""));

        return allWorkoutTypes.Except(activeWorkoutTypes).ToList();
    }

    private List<WorkoutTypeDto> GetAllWorkoutTypes()
    {
        return Enum.GetNames<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto(
                workoutType,
                WorkoutUtils.GetCategoryFromType(workoutType).GetDisplayName())
            ).ToList();
    }
}
