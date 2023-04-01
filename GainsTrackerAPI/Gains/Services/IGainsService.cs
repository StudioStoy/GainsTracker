using GainsTrackerAPI.Gains.Models;

namespace GainsTrackerAPI.Gains.Services;

public interface IGainsService
{
    public Task<List<GainsAccount>> GetAllGainsAccounts();
    public Task<List<Workout>> GetWorkoutsByUsername(string username);
}
