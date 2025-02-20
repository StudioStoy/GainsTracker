using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.UI.Services.API.Workouts;

public class CatalogService(ApiService api) : ICatalogService
{
    public async Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypes()
    {
        return await api.GetAsync<List<WorkoutTypeDto>>("/catalog/workouts") ?? [];
    }
}
