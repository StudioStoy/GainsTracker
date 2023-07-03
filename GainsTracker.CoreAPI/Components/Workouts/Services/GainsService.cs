using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Data;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public class GainsService : IGainsService
{
    private readonly BigBrainWorkout _bigBrain;
    private readonly IMeasurementService _measurementService;

    public GainsService(BigBrainWorkout bigBrain, IMeasurementService measurementService)
    {
        _bigBrain = bigBrain;
        _measurementService = measurementService;
    }

    public GainsAccount GetGainsAccountFromUser(string username)
    {
        return _bigBrain.GetGainsAccountByUsername(username);
    }

    public List<WorkoutDto> GetWorkoutsByUsername(string username)
    {
        string id = _bigBrain.GetGainsIdByUsername(username);
        return _bigBrain.GetWorkoutsByGainsId(id)
            .Select(w => w.ToDto())
            .ToList();
    }

    public string AddWorkoutToGainsAccount(string username, CreateWorkoutDto workoutDto)
    {
        GainsAccount gainsAccount = _bigBrain.GetGainsAccountByUsername(username);
        WorkoutTypeAlreadyUsed(gainsAccount.Id, workoutDto.WorkoutType);

        Workout workout = new(gainsAccount.Id, workoutDto.WorkoutType, workoutDto.WorkoutType.GetCategoryFromType(),
            new List<Measurement>());
        gainsAccount.AddWorkout(workout);

        _bigBrain.SaveContext();
        return workout.Id;
    }

    public WorkoutMeasurementsDto GetWorkoutMeasurementsById(string workoutId)
    {
        Workout workout = _bigBrain.GetWorkoutWithMeasurementsById(workoutId);
        return workout.ToMeasurementsListDto();
    }

    public void AddMeasurementToWorkout(string workoutId, CreateMeasurementDto dto)
    {
        Measurement measurement = MeasurementFactory.DeserializeMeasurementFromJson(dto.Category, dto.Data);
        _measurementService.ValidateMeasurement(measurement);

        Workout workout = _bigBrain.GetWorkoutById(workoutId);
        workout.AddNewMeasurement(measurement);

        _bigBrain.SaveContext();
    }

    private void WorkoutTypeAlreadyUsed(string gainsId, WorkoutType type)
    {
        List<Workout> workouts = _bigBrain.GetWorkoutsByGainsId(gainsId);
        if (workouts.Any(w => w.Type == type))
            throw new ConflictException($"Workout with type {type} is already added to this account!");
    }
}
