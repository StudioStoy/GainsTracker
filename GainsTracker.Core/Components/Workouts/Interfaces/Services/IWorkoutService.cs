using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Components.Workouts.Models;

namespace GainsTracker.Core.Components.Workouts.Interfaces.Services;

public interface IWorkoutService
{
    // Account
    Task<GainsAccount> GetGainsAccountFromUser(string username);

    // Workouts
    Task<List<WorkoutDto>> GetWorkoutsByUsername(string username);
    Task<WorkoutDto> AddWorkoutToGainsAccount(string username, CreateWorkoutDto workout);

    // Measurements
    Task<WorkoutMeasurementsDto> GetWorkoutMeasurementsById(Guid workoutId);
    Task AddMeasurementToWorkout(Guid id, CreateMeasurementDto measurementRequestDto);
}
