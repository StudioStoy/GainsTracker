using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public interface IGainsService
{
    // Account
    GainsAccount GetGainsAccountFromUser(string username);

    // Workouts
    Task<List<WorkoutDto>> GetWorkoutsByUsername(string username);
    void AddWorkoutToGainsAccount(string username, CreateWorkoutDto workout);

    // Measurements
    WorkoutMeasurementsDto GetWorkoutMeasurementsById(string workoutId);
    void AddMeasurementToWorkout(string id, MeasurementDto measurementRequestDto);

    // User
    void UpdateDisplayName(string userHandle, string newDisplayName);
}
