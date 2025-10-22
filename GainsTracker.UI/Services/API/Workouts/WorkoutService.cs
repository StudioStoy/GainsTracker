using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.UI.Services.API.Workouts;

public class WorkoutService(ApiService api) : IWorkoutService
{
    public async Task<List<WorkoutDto>> GetUserWorkouts() =>
        (await api.GetAsync<List<WorkoutDto>>("/workouts")).Value ?? [];

    public async Task<List<WorkoutDto>> GetWorkoutMeasurements(Guid workoutId) =>
        (await api.GetAsync<List<WorkoutDto>>($"/workouts/{workoutId}")).Value ?? [];
    
    public async Task<List<PersonalBestDto>> GetPersonalBests() =>
        (await api.GetAsync<List<PersonalBestDto>>("/workouts/personal-bests")).Value ?? [];

    public async Task<ApiResult> CreateNewWorkout(CreateNewWorkoutDto workout)
    {
        // TODO: parse distances to meters, if km was selected. Maybe make it an extension for the dto's.
        return await api.PostAsync("/workouts", workout);
    }

    public async Task<ApiResult> AddMeasurementToWorkout(Guid workoutId, CreateMeasurementDto measurement)
    {
        return await api.PostAsync($"/workouts/{workoutId}/measurements", measurement);        
    }

    public async Task RemoveWorkoutById(Guid workoutId)
    {
        await api.DeleteAsync($"/workouts/{workoutId}");
    }
}
