#region

using GainsTracker.Core.Workouts.Models.Workouts;

#endregion

namespace GainsTracker.Core.Workouts.Interfaces.Repositories;

public interface IWorkoutRepository : IGenericRepository<Workout>
{
    Task<List<Workout>> GetWorkoutsByGainsId(Guid gainsId);
    Task<Workout> GetWorkoutById(Guid id);
    Task<Workout> GetWorkoutWithMeasurementsById(Guid id);
}
