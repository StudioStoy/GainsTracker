using GainsTracker.CoreAPI.Components.Workout.Models;
using GainsTracker.CoreAPI.Components.Workout.Services.Dto;

namespace GainsTracker.CoreAPI.Components.Workout.Services;

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
