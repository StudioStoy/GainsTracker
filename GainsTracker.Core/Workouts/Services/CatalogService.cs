using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Services;

public class CatalogService(IWorkoutRepository repository) : ICatalogService
{
    public async Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypesByGainsId(Guid gainsId)
    {
        var allWorkoutTypes = GetAllWorkoutTypes();
        var workouts = (await repository.GetUsedWorkoutTypesByGainsId(gainsId))
            .Select(type => new WorkoutTypeDto(type, type.GetCategory()));

        return allWorkoutTypes.Except(workouts).ToList();
    }

    private static List<WorkoutTypeDto> GetAllWorkoutTypes()
    {
        return Enum.GetValues<WorkoutType>()
            .Select(workoutType => new WorkoutTypeDto(
                workoutType,
                workoutType.GetCategory())
            ).ToList();
    }
}
