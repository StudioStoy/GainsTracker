using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Gains.Interfaces.Services;
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
    public async Task<List<WorkoutDto>> GetWorkoutsByUsername(string username)
    {
        var id = await gainsService.GetGainsIdByUsername(username);
        return (await workoutRepository.GetWorkoutsByGainsId(id))
            .Select(w => w.ToDto())
            .ToList();
    }

    public async Task<WorkoutDto> AddWorkoutToGainsAccount(string username, CreateWorkoutDto workoutDto)
    {
        var gainsAccount = await gainsService.GetGainsAccountByUserHandle(username);
        await WorkoutTypeAlreadyUsed(gainsAccount.Id, workoutDto.WorkoutType);

        Workout workout = new(gainsAccount.Id, workoutDto.WorkoutType, workoutDto.WorkoutType.GetCategoryFromType(),
            []);
        gainsAccount.AddWorkout(workout);

        await workoutRepository.AddAsync(workout);
        await gainsService.UpdateGainsAccount(gainsAccount);

        return new WorkoutDto
        (
            Id: workout.Id,
            GainsAccountId: gainsAccount.Id,
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
            Measurements: workout.Measurements
                .Select(m => new MeasurementDto
                (
                    Id: m.Id.ToString(),
                    WorkoutId: workout.Id.ToString(),
                    Category: m.Category,
                    TimeOfRecord: m.TimeOfRecord,
                    Notes: m.Notes,
                    Data: MeasurementFactory.SerializeMeasurementToJson(m)
                )).ToList()
        );
    }

    public async Task AddMeasurementToWorkout(Guid workoutId, CreateMeasurementDto dto)
    {
        var measurement = MeasurementFactory.DeserializeMeasurementFromJson(dto.Category, dto.Data);
        measurementValidationService.ValidateMeasurement(measurement);

        var workout = await workoutRepository.GetWorkoutById(workoutId);
        workout.AddNewMeasurement(measurement);

        await measurementRepository.AddAsync(measurement);
        await workoutRepository.UpdateAsync(workout);
    }

    private async Task WorkoutTypeAlreadyUsed(Guid gainsId, WorkoutType type)
    {
        var workouts = await workoutRepository.GetWorkoutsByGainsId(gainsId);
        if (workouts.Any(w => w.Type == type))
            throw new ConflictException($"Workout with type {type} is already added to this account!");
    }
}
