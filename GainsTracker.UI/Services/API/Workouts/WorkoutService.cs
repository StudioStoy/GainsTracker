using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.UI.Services.API.Workouts;

public class WorkoutService(ApiService api) : IWorkoutService
{
    public async Task<List<WorkoutDto>> GetUserWorkouts() =>
        await api.GetAsync<List<WorkoutDto>>("/workouts") ?? [];

    public async Task<List<PersonalBestDto>> GetPersonalBests() =>
        await api.GetAsync<List<PersonalBestDto>>("/workouts/personal-bests") ?? [];

    public async Task CreateWorkout(AddNewWorkoutDto workout) =>
        await api.PostAsync("/workouts", workout);
}
