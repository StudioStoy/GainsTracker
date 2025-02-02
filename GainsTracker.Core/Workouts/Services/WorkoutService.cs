using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Workouts.Extensions;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core.Workouts.Services;

public class WorkoutService(
    IWorkoutRepository repository,
    IMeasurementService measurementService,
    IGainsService gainsService)
    : IWorkoutService
{
    public async Task<List<WorkoutDto>> GetWorkoutsByGainsId(Guid gainsId)
    {
        return (await repository.GetWorkoutsByGainsId(gainsId))
            .Select(w => w.ToDto())
            .ToList();
    }

    public async Task<WorkoutDto> AddWorkoutToGainsAccount(Guid gainsId, AddNewWorkoutDto workoutDto)
    {
        var gainsAccount = await gainsService.GetGainsAccountById(gainsId);
        await WorkoutTypeAlreadyUsed(gainsAccount.Id, workoutDto.WorkoutType);

        var workout = new Workout(gainsAccount.Id, workoutDto.WorkoutType, workoutDto.WorkoutType.GetCategoryFromType(),
            []);
        gainsAccount.AddWorkout(workout);

        await repository.AddAsync(workout);
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
        var workout = await repository.GetWorkoutWithMeasurementsById(workoutId);

        return new WorkoutMeasurementsDto
        (
            Id: workout.Id,
            Measurements: workout.Measurements.Select(m => m.ToDto()).ToList()
        );
    }

    public async Task<MeasurementDto> AddMeasurementToWorkout(Guid workoutId, CreateMeasurementDto measurementDto)
    {
        var measurement = await measurementService.CreateMeasurement(measurementDto);

        var workout = await repository.GetWorkoutById(workoutId);
        workout.AddNewMeasurement(measurement);

        await repository.UpdateAsync(workout);

        return measurement.ToDto();
    }

    public async Task<List<PersonalBestDto>> GetAllPersonalBestsByGainsId(Guid gainsId)
    {
        return (await repository.GetAllPersonalBestsByGainsId(gainsId))
            .Select(w => new PersonalBestDto(w.Id, w.Type.ToString(), w.PersonalBest!.ToDto()))
            .ToList();
    }

    private async Task WorkoutTypeAlreadyUsed(Guid gainsId, WorkoutType type)
    {
        var workouts = await repository.GetWorkoutsByGainsId(gainsId);
        if (workouts.Any(w => w.Type == type))
            throw new ConflictException($"Workout with type {type} is already added to this account!");
    }
}
