using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Services.Dto;

namespace GainsTrackerAPI.Components.Gains.Services;

public interface IGainsService
{
    public GainsAccount GetGainsAccountFromUser(string username);
    public Task<List<WorkoutDto>> GetWorkoutsByUsername(string username);
    public WorkoutMeasurementsDto GetWorkoutMeasurementsById(string workoutId);
    public void AddWorkoutToGainsAccount(string username, WorkoutDto workout);
    void AddMeasurementToWorkout(string id, MeasurementDto measurementRequestDto);
}
