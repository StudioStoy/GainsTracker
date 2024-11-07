using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Components.Workouts.Models.Workouts;
using GainsTracker.Data.Workouts;

namespace GainsTracker.Core.Components.Workouts.Services;

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
        IEnumerable<WorkoutTypeDto> activeWorkoutTypes = _bigBrain.GetWorkoutsByGainsId(gainsId).Select(w => new WorkoutTypeDto(w.Type.ToString(), ""));

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
