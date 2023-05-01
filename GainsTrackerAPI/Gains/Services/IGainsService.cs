using GainsTrackerAPI.Gains.Models;

namespace GainsTrackerAPI.Gains.Services;

public interface IGainsService
{
    public Task<GainsAccount> GetGainsAccountFromUser(string username);
    public Task<List<Workout>> GetWorkoutsByUsername(string username);
}
