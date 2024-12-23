﻿using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Core.Workouts.Interfaces.Services;

public interface IWorkoutService
{
    // Workouts
    Task<List<WorkoutDto>> GetWorkoutsByUsername(string username);
    Task<WorkoutDto> AddWorkoutToGainsAccount(string username, CreateWorkoutDto workout);

    // Measurements
    Task<WorkoutMeasurementsDto> GetWorkoutMeasurementsById(Guid workoutId);
    Task<MeasurementDto> AddMeasurementToWorkout(Guid id, CreateMeasurementDto measurementRequestDto);
}
