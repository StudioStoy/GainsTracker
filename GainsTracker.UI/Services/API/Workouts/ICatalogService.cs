using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.UI.Services.API.Workouts;

public interface ICatalogService
{
    Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypes();
}
