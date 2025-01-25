using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface ICatalogService
{
    Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypesByGainsId(Guid gainsId);
}
