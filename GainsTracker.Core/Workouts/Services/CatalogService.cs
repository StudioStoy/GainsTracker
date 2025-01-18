using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Services;

public class CatalogService(IWorkoutRepository repository) : ICatalogService
{
    public async Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypesByGainsId(Guid gainsId)
    {
        var allWorkoutTypes = GetAllWorkoutTypes();
        var workouts = await repository.GetWorkoutsByGainsId(gainsId);
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
