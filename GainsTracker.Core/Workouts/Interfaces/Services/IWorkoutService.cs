using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface IWorkoutService
{
    // Workouts
    Task<List<WorkoutDto>> GetWorkoutsByGainsId(Guid gainsId);
    Task<WorkoutDto> AddWorkoutToGainsAccount(Guid gainsId, AddNewWorkoutDto workout);

    // Measurements
    Task<WorkoutMeasurementsDto> GetWorkoutMeasurementsById(Guid workoutId);
    Task<MeasurementDto> AddMeasurementToWorkout(Guid id, CreateMeasurementDto measurementDto);
    Task<List<PersonalBestDto>> GetAllPersonalBestsByGainsId(Guid gainsId);
}
