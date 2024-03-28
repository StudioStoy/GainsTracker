using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public interface IGainsService
{
    // Account
    GainsAccount GetGainsAccountFromUser(string username);

    // Workouts
    List<WorkoutDto> GetWorkoutsByUsername(string username);
    WorkoutDto AddWorkoutToGainsAccount(string username, CreateWorkoutDto workout);

    // Measurements
    WorkoutMeasurementsDto GetWorkoutMeasurementsById(string workoutId);
    void AddMeasurementToWorkout(string id, CreateMeasurementDto measurementRequestDto);
}
