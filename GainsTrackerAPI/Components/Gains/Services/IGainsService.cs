using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Services;

public interface IGainsService
{
    public GainsAccount GetGainsAccountFromUser(string username);
    public Task<List<Workout>> GetWorkoutsByUsername(string username);
}
