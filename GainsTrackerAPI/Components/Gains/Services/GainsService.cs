using GainsTrackerAPI.Components.Gains.Data;
using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Models.Measurements;
using GainsTrackerAPI.Components.Gains.Models.Workouts;
using GainsTrackerAPI.Components.Gains.Services.Dto;

namespace GainsTrackerAPI.Components.Gains.Services;

public class GainsService : IGainsService
{
    private readonly BigBrainWorkout _bigBrain;

    public GainsService(BigBrainWorkout bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public GainsAccount GetGainsAccountFromUser(string username)
    {
        return _bigBrain.GetGainsAccountByUsername(username);
    }

    public async Task<List<Workout>> GetWorkoutsByUsername(string username)
    {
        string id = _bigBrain.GetGainsIdByUsername(username);
        return await _bigBrain.GetWorkoutsByGainsId(id);
    }

    public void AddWorkoutToGainsAccount(string username, WorkoutDto workoutDto)
    {
        GainsAccount gainsAccount = GetGainsAccountFromUser(username);
        Workout workout = new(gainsAccount.Id, workoutDto.WorkoutType, new List<Measurement>());
        gainsAccount.AddWorkout(workout);

        _bigBrain.SaveContext();
    }
}
