using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Data;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using Microsoft.OpenApi.Extensions;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public class WorkoutCatalogService : IWorkoutCatalogService
{
    private readonly BigBrainWorkout _bigBrain;

    public WorkoutCatalogService(BigBrainWorkout bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public List<WorkoutTypeDto> GetAvailableWorkoutTypesForUser(string username)
    {
        var gainsId = _bigBrain.GetGainsIdByUsername(username);
        var allWorkoutTypes = GetAllWorkoutTypes();
        var activeWorkoutTypes = _bigBrain.GetWorkoutsByGainsId(gainsId).Select(w => new WorkoutTypeDto(w.WorkoutType.ToString(), ""));

        return allWorkoutTypes.Except(activeWorkoutTypes).ToList();
    }

    public List<WorkoutTypeDto> GetAllWorkoutTypes()
    {
        return Enum.GetNames<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto(
                workoutType,
                WorkoutUtils.GetCategoryFromType(workoutType).GetDisplayName())
            ).ToList();
    }
}
