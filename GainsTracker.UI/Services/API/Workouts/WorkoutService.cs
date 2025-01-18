using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.UI.Services.API.Workouts;

public class WorkoutService(ApiService api) : IWorkoutService
{
    public async Task<List<WorkoutDto>> GetUserWorkouts()
    {
        return await api.GetAsync<List<WorkoutDto>>("/workouts") ?? [];
    }

    public Task<List<MeasurementDto>> GetPersonalBests() => throw new NotImplementedException();
}
