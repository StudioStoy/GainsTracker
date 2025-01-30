using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Workouts.Extensions;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Services;

public class WorkoutService(
    IWorkoutRepository workoutRepository,
    IMeasurementRepository measurementRepository,
    IMeasurementValidationService measurementValidationService,
    IGainsService gainsService)
    : IWorkoutService
{
    public async Task<List<WorkoutDto>> GetWorkoutsByGainsId(Guid gainsId)
    {
        return (await workoutRepository.GetWorkoutsByGainsId(gainsId))
            .Select(w => w.ToDto())
            .ToList();
    }

    public async Task<WorkoutDto> AddWorkoutToGainsAccount(Guid gainsId, AddNewWorkoutDto workoutDto)
    {
        var gainsAccount = await gainsService.GetGainsAccountById(gainsId);
        await WorkoutTypeAlreadyUsed(gainsAccount.Id, workoutDto.WorkoutType);

        Workout workout = new(gainsAccount.Id, workoutDto.WorkoutType, workoutDto.WorkoutType.GetCategoryFromType(),
            []);
        gainsAccount.AddWorkout(workout);

        await workoutRepository.AddAsync(workout);
        await gainsService.UpdateGainsAccount(gainsAccount);

        return new WorkoutDto
        (
            Id: workout.Id,
            Type: workout.Type,
            Category: workout.Category
        );
    }

    public async Task<WorkoutMeasurementsDto> GetWorkoutMeasurementsById(Guid workoutId)
    {
        var workout = await workoutRepository.GetWorkoutWithMeasurementsById(workoutId);

        return new WorkoutMeasurementsDto
        (
            Id: workout.Id,
            Measurements: workout.Measurements.Select(m => m.ToDto()).ToList()
        );
    }

    public async Task<MeasurementDto> AddMeasurementToWorkout(Guid workoutId, CreateMeasurementDto measurementDto)
    {
        var measurement = measurementDto.ToModel();
        measurementValidationService.ValidateMeasurement(measurement);

        var workout = await workoutRepository.GetWorkoutById(workoutId);
        workout.AddNewMeasurement(measurement);

        await measurementRepository.AddAsync(measurement);
        await workoutRepository.UpdateAsync(workout);

        return measurement.ToDto();
    }

    private async Task WorkoutTypeAlreadyUsed(Guid gainsId, WorkoutType type)
    {
        var workouts = await workoutRepository.GetWorkoutsByGainsId(gainsId);
        if (workouts.Any(w => w.Type == type))
            throw new ConflictException($"Workout with type {type} is already added to this account!");
    }
}
