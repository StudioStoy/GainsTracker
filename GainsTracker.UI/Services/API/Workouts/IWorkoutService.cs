using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.UI.Services.API.Workouts;

public interface IWorkoutService
{
    public Task<List<WorkoutDto>> GetUserWorkouts();
    public Task<List<MeasurementDto>> GetPersonalBests();
}
