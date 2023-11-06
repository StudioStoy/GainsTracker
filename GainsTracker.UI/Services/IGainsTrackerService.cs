using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.UI.Services;

public interface IGainsTrackerService
{
    public Task<List<WorkoutDto>> GetUserWorkouts();
    public Task<List<MeasurementDto>> GetPersonalBests();
}
