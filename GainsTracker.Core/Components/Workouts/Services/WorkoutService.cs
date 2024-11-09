using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Components.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Components.Workouts.Interfaces.Services;
using GainsTracker.Core.Components.Workouts.Models;
using GainsTracker.Core.Components.Workouts.Models.Measurements;
using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Services;

public class WorkoutService(IWorkoutBigBrain bigBrain, IMeasurementValidationService measurementValidationService)
    : IWorkoutService
{
    public async Task<GainsAccount> GetGainsAccountFromUser(string username)
    {
        return await bigBrain.GetGainsAccountByUserHandle(username);
    }

    public async Task<List<WorkoutDto>> GetWorkoutsByUsername(string username)
    {
        var id = await bigBrain.GetGainsIdByUsername(username);
        return (await bigBrain.GetWorkoutsByGainsId(id))
            .Select(w => w.ToDto())
            .ToList();
    }

    public async Task<WorkoutDto> AddWorkoutToGainsAccount(string username, CreateWorkoutDto workoutDto)
    {
        GainsAccount gainsAccount = await bigBrain.GetGainsAccountByUserHandle(username);
        await WorkoutTypeAlreadyUsed(gainsAccount.Id, workoutDto.WorkoutType);

        Workout workout = new(gainsAccount.Id, workoutDto.WorkoutType, workoutDto.WorkoutType.GetCategoryFromType(), []);
        gainsAccount.AddWorkout(workout);

        await bigBrain.SaveContext();
        return new WorkoutDto(gainsAccount.Id)
        {
            Type = workout.Type,
            Category = workout.Category,
            Id = workout.Id
        };
    }

    public async Task<WorkoutMeasurementsDto> GetWorkoutMeasurementsById(Guid workoutId)
    {
        Workout workout = await bigBrain.GetWorkoutWithMeasurementsById(workoutId);
        return new WorkoutMeasurementsDto
        {
            Id = workout.Id,
            Measurements = workout.Measurements
                .Select(m => new MeasurementDto
                {
                    Id = m.Id,
                    WorkoutId = m.WorkoutId,
                    Category = m.Category,
                    TimeOfRecord = m.TimeOfRecord,
                    Notes = m.Notes,
                    Data = MeasurementFactory.SerializeMeasurementToJson(m)
                }).ToList()
        };
    }

    public async Task AddMeasurementToWorkout(Guid workoutId, CreateMeasurementDto dto)
    {
        Measurement measurement = MeasurementFactory.DeserializeMeasurementFromJson(dto.Category, dto.Data);
        measurementValidationService.ValidateMeasurement(measurement);

        var workout = await bigBrain.GetWorkoutById(workoutId);
        workout.AddNewMeasurement(measurement);

        await bigBrain.SaveContext();
    }

    private async Task WorkoutTypeAlreadyUsed(Guid gainsId, WorkoutType type)
    {
        List<Workout> workouts = await bigBrain.GetWorkoutsByGainsId(gainsId);
        if (workouts.Any(w => w.Type == type))
            throw new ConflictException($"Workout with type {type} is already added to this account!");
    }
}
