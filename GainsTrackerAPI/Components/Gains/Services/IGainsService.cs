using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Services.Dto;

namespace GainsTrackerAPI.Components.Gains.Services;

public interface IGainsService
{
    // Account
    GainsAccount GetGainsAccountFromUser(string username);
    
    // Workouts
    Task<List<WorkoutDto>> GetWorkoutsByUsername(string username);
    void AddWorkoutToGainsAccount(string username, WorkoutDto workout);
    
    // Measurements
    WorkoutMeasurementsDto GetWorkoutMeasurementsById(string workoutId);
    void AddMeasurementToWorkout(string id, MeasurementDto measurementRequestDto);
    
    // User
    void UpdateDisplayName(string userHandle, string newDisplayName);
}
