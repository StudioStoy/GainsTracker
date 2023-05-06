using GainsTrackerAPI.Components.Gains.Data;
using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Models.Measurements;
using GainsTrackerAPI.Components.Gains.Models.Workouts;
using GainsTrackerAPI.Components.Gains.Services.Dto;

namespace GainsTrackerAPI.Components.Gains.Services;

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

    public async Task<List<WorkoutDto>> GetWorkoutsByUsername(string username)
    {
        string id = _bigBrain.GetGainsIdByUsername(username);
        return (await _bigBrain.GetWorkoutsByGainsId(id))
            .Select(WorkoutDto.FromWorkout)
            .ToList();
    }

    public void AddWorkoutToGainsAccount(string username, WorkoutDto workoutDto)
    {
        GainsAccount gainsAccount = GetGainsAccountFromUser(username);
        Workout workout = new(gainsAccount.Id, workoutDto.WorkoutType, new List<Measurement>());
        gainsAccount.AddWorkout(workout);

        _bigBrain.SaveContext();
    }

    public WorkoutMeasurementsDto GetWorkoutMeasurementsById(string workoutId)
    {
        Workout workout = _bigBrain.GetWorkoutWithMeasurementsById(workoutId);
        return WorkoutMeasurementsDto.FromWorkout(workout);
    }

    public void AddMeasurementToWorkout(string workoutId, MeasurementDto dto)
    {
        Measurement measurement = MeasurementFactory.DeserializeMeasurementFromJson(dto.Category, dto.Data);
        _measurementService.ValidateMeasurement(measurement);

        Workout workout = _bigBrain.GetWorkoutById(workoutId);
        workout.AddNewMeasurement(measurement);

        _bigBrain.SaveContext();
    }
}
