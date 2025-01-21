using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface IWorkoutService
{
    // Workouts
    Task<List<WorkoutDto>> GetWorkoutsByGainsId(Guid gainsId);
    Task<WorkoutDto> AddWorkoutToGainsAccount(Guid gainsId, AddNewWorkoutDto workout);

    // Measurements
    Task<WorkoutMeasurementsDto> GetWorkoutMeasurementsById(Guid workoutId);
    Task<MeasurementDto> AddMeasurementToWorkout(Guid id, CreateMeasurementDto measurementRequestDto);
}
