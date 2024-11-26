﻿using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Interfaces.Repositories;

public interface IWorkoutBigBrain : IGenericRepository<Workout>
{
    Task<List<Workout>> GetWorkoutsByGainsId(Guid gainsId);
    Task<Workout> GetWorkoutById(Guid id);
    Task<Workout> GetWorkoutWithMeasurementsById(Guid id);
}
