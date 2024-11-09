using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Interfaces.Repositories;

public interface IWorkoutBigBrain : IBigBrain<Workout>
{
    Task<List<Workout>> GetWorkoutsByGainsId(Guid gainsId);
    Task<Workout> GetWorkoutById(Guid id);
    Task<Workout> GetWorkoutWithMeasurementsById(Guid id);
}
