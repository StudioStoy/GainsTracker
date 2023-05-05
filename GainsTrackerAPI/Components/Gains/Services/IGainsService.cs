using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Models.Workouts;
using GainsTrackerAPI.Components.Gains.Services.Dto;

namespace GainsTrackerAPI.Components.Gains.Services;

public interface IGainsService
{
    public GainsAccount GetGainsAccountFromUser(string username);
    public Task<List<Workout>> GetWorkoutsByUsername(string username);
    public void AddWorkoutToGainsAccount(string username, WorkoutDto workout);
}
