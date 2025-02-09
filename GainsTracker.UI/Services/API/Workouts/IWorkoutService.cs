using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.UI.Services.API.Workouts;

public interface IWorkoutService
{
    public Task<List<WorkoutDto>> GetUserWorkouts();
    public Task<List<PersonalBestDto>> GetPersonalBests();
    public Task CreateWorkout(AddNewWorkoutDto workout);
}
