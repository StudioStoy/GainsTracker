﻿using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Interfaces.Repositories;

public interface IWorkoutRepository : IGenericRepository<Workout>
{
    Task<List<Workout>> GetWorkoutsByGainsId(Guid gainsId);
    Task<List<WorkoutType>> GetUsedWorkoutTypesByGainsId(Guid gainsId);
    Task<Workout> GetWorkoutById(Guid id);
    Task<Workout> GetWorkoutWithMeasurementsById(Guid id);
    Task<List<Workout>> GetAllPersonalBestsByGainsId(Guid gainsId);
}
